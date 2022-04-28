using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Dynamic;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>   A dynamic data configuration extension. </summary>
public static class DynamicDataConfigurationExtension
{
    /// <summary>   Configure dynamic data provider. </summary>
    /// <param name="services">             The services. </param>
    /// <param name="configurationManager"> Manager for configuration. </param>
    /// <returns>   An IServiceCollection. </returns>
    private static IServiceCollection ConfigureDynamicDataProvider(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        // provider-selection is required
        services.Configure<DynamicDataOptions>(configurationManager, true);

        // provider-configurations are optional (at least here)
        services.Configure<EfSqlServiceOptions>(configurationManager);
        services.Configure<LiteDbServiceOptions>(configurationManager);
        services.Configure<MssqlDapperServiceOptions>(configurationManager);
        services.Configure<MysqlDapperServiceOptions>(configurationManager);
        services.Configure<PgsqlDapperServiceOptions>(configurationManager);
        services.Configure<SqliteDapperServiceOptions>(configurationManager);

        return services;
    }

    /// <summary>
    ///     Configure dynamic data provider.
    /// </summary>
    /// <typeparam name="TDataService">     Type of the data service. </typeparam>
    /// <typeparam name="TDynamicOptions">  Type of the dynamic options. </typeparam>
    /// <param name="services">             The services. </param>
    /// <param name="configurationManager"> Manager for configuration. </param>
    /// <param name="dataServiceProvider">  The data service provider. </param>
    /// <returns>
    ///     An IServiceCollection.
    /// </returns>
    private static IServiceCollection ConfigureDynamicDataProvider<TDataService, TDynamicOptions>(
        this IServiceCollection services,
        ConfigurationManager configurationManager, Func<IServiceProvider, TDataService> dataServiceProvider)
        where TDataService : class, IDataService
        where TDynamicOptions : class, IDynamicDataOptions, new()
    {
        services.Configure<TDynamicOptions>(configurationManager);

        services.AddTransient<IDataService>(dataServiceProvider);
        services.AddSingleton(dataServiceProvider);
        return ConfigureDynamicDataProvider(services, configurationManager);
    }

    /// <summary>
    ///     Configure dynamic data provider.
    /// </summary>
    /// <typeparam name="TDataService">     Type of the data service. </typeparam>
    /// <typeparam name="TDynamicOptions">  Type of the dynamic options. </typeparam>
    /// <param name="services">             The services. </param>
    /// <param name="configurationManager"> Manager for configuration. </param>
    /// <param name="dataServiceProvider">  The data service provider. </param>
    /// <returns>
    ///     An IServiceCollection.
    /// </returns>
    public static IServiceCollection ConfigureDynamicDataProvider<TDataService, TDynamicOptions>(
        this IServiceCollection services,
        ConfigurationManager configurationManager,
        Func<IOptionsMonitor<IDynamicDataOptions>, IServiceProvider, TDataService> dataServiceProvider)
        where TDataService : class, IDataService
        where TDynamicOptions : class, IDynamicDataOptions, new()
    {
        return ConfigureDynamicDataProvider<TDataService, TDynamicOptions>(services, configurationManager, provider =>
        {
            IOptionsMonitor<IDynamicDataOptions> preferredOptions =
                provider.GetService<IOptionsMonitor<TDynamicOptions>>();
            IOptionsMonitor<IDynamicDataOptions> fallbackOptions =
                provider.GetRequiredService<IOptionsMonitor<DynamicDataOptions>>();
            var dynamicOptions = new FallbackDynamicDataOptions(preferredOptions, fallbackOptions);

            var service = dataServiceProvider(dynamicOptions, provider);

            if (!service.SupportsMigration) return service;

            var migrator = service.GetMigrator();
            if (migrator.CurrentVersion == migrator.MaximumVersion) return service;

            if (dynamicOptions.CurrentValue.AutoMigrate)
                DataMigrationSingleton.Instance.OnMigrationPossible(migrator, service);
            else
                DataMigrationSingleton.Instance.OnVersionMismatch(migrator, service);

            return service;
        });
    }
}