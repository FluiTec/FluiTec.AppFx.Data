using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
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
            services.Configure<LiteDbServiceOptions>(configurationManager);
            services.Configure<MssqlDapperServiceOptions>(configurationManager);
            services.Configure<MysqlDapperServiceOptions>(configurationManager);
            services.Configure<PgsqlDapperServiceOptions>(configurationManager);
            services.Configure<SqliteDapperServiceOptions>(configurationManager);

            return services;
        }

        /// <summary>   Configure dynamic data provider. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        /// <returns>   An IServiceCollection. </returns>
        private static IServiceCollection ConfigureDynamicDataProvider<TDataService>(this IServiceCollection services,
            ConfigurationManager configurationManager, Func<IServiceProvider, TDataService> dataServiceProvider)
            where TDataService : class, IDataService
        {
            services.AddSingleton(dataServiceProvider);
            return ConfigureDynamicDataProvider(services, configurationManager);
        }

        /// <summary>   Configure dynamic data provider. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="dataServiceProvider">  The data service provider. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection ConfigureDynamicDataProvider<TDataService>(this IServiceCollection services,
            ConfigurationManager configurationManager,
            Func<DynamicDataOptions, IServiceProvider, TDataService> dataServiceProvider)
            where TDataService : class, IDataService
        {
            return ConfigureDynamicDataProvider(services, configurationManager, provider =>
            {
                var dynamicOptions = provider.GetRequiredService<DynamicDataOptions>();
                var service = dataServiceProvider(dynamicOptions, provider);

                if (dynamicOptions.AutoMigrate && service.SupportsMigration) 
                    service.GetMigrator().Migrate();

                return service;
            });
        }
    }
}