using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FluiTec.AppFx.Data;

/// <summary>   An extensions. </summary>
public static class Extensions
{
    /// <summary>   An IServiceCollection extension method that adds a data options. </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <param name="services">         The services to act on. </param>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="sectionName">      (Optional) Name of the section. </param>
    /// <returns>   An IServiceCollection. </returns>
    public static IServiceCollection AddDataOptions<TDataService>(this IServiceCollection services,
        IConfiguration configuration, string sectionName = "DataOptions") where TDataService : IDataService
    {
        services.Configure<DataOptions<TDataService>>(GetSection(configuration, $"{sectionName}:{typeof(TDataService).Name}", sectionName));
        return services;
    }

    /// <summary>
    ///     An IServiceCollection extension method that adds a connection string options 2.
    /// </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <param name="services">         The services to act on. </param>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="sectionName">      (Optional) Name of the section. </param>
    /// <returns>   An IServiceCollection. </returns>
    public static IServiceCollection AddConnectionStringOptions<TDataService>(this IServiceCollection services,
        IConfiguration configuration, string sectionName = "ConnectionStringOptions") where TDataService : IDataService
    {
        services.Configure<ConnectionStringOptions2<TDataService>>(GetSection(configuration, $"{sectionName}:{typeof(TDataService).Name}", sectionName));
        return services;
    }

    /// <summary>   An IServiceCollection extension method that adds a data service. </summary>
    /// <typeparam name="TInterface">   Type of the interface. </typeparam>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <param name="services"> The services to act on. </param>
    /// <returns>   An IServiceCollection. </returns>
    public static IServiceCollection AddDataService<TInterface, TDataService>(this IServiceCollection services)
        where TInterface : class, IDataService
        where TDataService : class, TInterface
    {
        services.AddSingleton<TDataService>();
        services.AddSingleton<IDataService>(provider => provider.GetRequiredService<TDataService>());
        services.AddSingleton<TInterface>(provider => provider.GetRequiredService<TDataService>());

        return services;
    }

    /// <summary>   An IServiceCollection extension method that adds a data provider. </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    /// <typeparam name="TProvider">    Type of the provider. </typeparam>
    /// <param name="services">     The services to act on. </param>
    /// <returns>   An IServiceCollection. </returns>
    public static IServiceCollection AddDataProvider<TDataService, TUnitOfWork, TProvider>(
        this IServiceCollection services)
        where TDataService : IDataService
        where TUnitOfWork : IUnitOfWork<TDataService>
        where TProvider : class, IDataProvider<TDataService, TUnitOfWork>
    {
        services.AddSingleton<IDataProvider<TDataService, TUnitOfWork>, TProvider>();
        services.TryAddSingleton<DataProviderResolver<TDataService, TUnitOfWork>>();
        return services;
    }

    /// <summary>   Gets a section. </summary>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="preferredSection"> The preferred section. </param>
    /// <param name="fallbackSection">  The fallback section. </param>
    /// <returns>   The section. </returns>
    private static IConfigurationSection GetSection(IConfiguration configuration, string preferredSection,
        string fallbackSection)
    {
        var preferred = configuration.GetSection(preferredSection);
        return preferred.Exists() ? preferred : configuration.GetRequiredSection(fallbackSection);
    }
}