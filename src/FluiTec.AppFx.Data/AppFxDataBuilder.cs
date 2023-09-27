using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data;

/// <summary>   An application effects data builder. </summary>
/// <typeparam name="TInterface">   Type of the interface. </typeparam>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
public class AppFxDataBuilder<TInterface, TDataService>
    where TInterface : class, IDataService
    where TDataService : class, TInterface
{
    /// <summary>   Constructor. </summary>
    /// <param name="services"> The services. </param>
    public AppFxDataBuilder(IServiceCollection services)
    {
        Services = services.AddDataService<TInterface, TDataService>();
    }

    /// <summary>   Gets the services. </summary>
    /// <value> The services. </value>
    public IServiceCollection Services { get; protected set; }

    /// <summary>   With configuration. </summary>
    /// <param name="configuration">            The configuration. </param>
    /// <param name="optionsSection">           (Optional) The options section. </param>
    /// <param name="connectionStringsSection"> (Optional) The connection strings section. </param>
    /// <returns>   An AppFxDataBuilder&lt;TInterface,TDataService&gt; </returns>
    public AppFxDataBuilder<TInterface, TDataService> WithConfiguration(IConfiguration configuration,
        string optionsSection = "DataOptions", string connectionStringsSection = "ConnectionStringOptions")
    {
        return
            WithOptions(configuration, optionsSection)
                .WithConnectionStrings(configuration, connectionStringsSection);
    }

    /// <summary>   With options. </summary>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="sectionName">      (Optional) Name of the section. </param>
    /// <returns>   An AppFxDataBuilder&lt;TInterface,TDataService&gt; </returns>
    public AppFxDataBuilder<TInterface, TDataService> WithOptions(IConfiguration configuration,
        string sectionName = "DataOptions")
    {
        Services = Services.AddDataOptions<TInterface>(configuration, sectionName);
        return this;
    }

    /// <summary>   With connection strings. </summary>
    /// <param name="configuration">    The configuration. </param>
    /// <param name="sectionName">      (Optional) Name of the section. </param>
    /// <returns>   An AppFxDataBuilder&lt;TInterface,TDataService&gt; </returns>
    public AppFxDataBuilder<TInterface, TDataService> WithConnectionStrings(IConfiguration configuration,
        string sectionName = "ConnectionStringOptions")
    {
        Services = Services.AddConnectionStringOptions<TInterface>(configuration, sectionName);
        return this;
    }

    /// <summary>   With provider. </summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    /// <typeparam name="TProvider">    Type of the provider. </typeparam>
    /// <returns>   An AppFxDataBuilder&lt;TInterface,TDataService&gt; </returns>
    public AppFxDataBuilder<TInterface, TDataService> WithProvider<TUnitOfWork, TProvider>()
        where TUnitOfWork : IUnitOfWork<TInterface>
        where TProvider : class, IDataProvider<TInterface, TUnitOfWork>
    {
        Services = Services.AddDataProvider<TInterface, TUnitOfWork, TProvider>();
        return this;
    }
}