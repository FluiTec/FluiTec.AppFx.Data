using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.DataProviders;

/// <summary>   A data provider resolver. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public class DataProviderResolver<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    private readonly DataOptions<TDataService> _options = null!;

    /// <summary>   Constructor. </summary>
    /// <param name="options">      The options. </param>
    /// <param name="providers">    The providers. </param>
    public DataProviderResolver(DataOptions<TDataService> options,
        IEnumerable<IDataProvider<TDataService, TUnitOfWork>> providers)
    {
        _options = options;
        Providers = providers;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="optionsMonitor">   The options monitor. </param>
    /// <param name="providers">        The providers. </param>
    public DataProviderResolver(IOptionsMonitor<DataOptions<TDataService>> optionsMonitor,
        IEnumerable<IDataProvider<TDataService, TUnitOfWork>> providers)
    {
        OptionsMonitor = optionsMonitor;
        Providers = providers;
    }

    /// <summary>   Gets the options monitor. </summary>
    /// <value> The options monitor. </value>
    public IOptionsMonitor<DataOptions<TDataService>>? OptionsMonitor { get; }

    /// <summary>   Gets options for controlling the operation. </summary>
    /// <value> The options. </value>
    public DataOptions<TDataService> Options => OptionsMonitor != null ? OptionsMonitor.CurrentValue : _options;

    /// <summary>   Gets the providers. </summary>
    /// <value> The providers. </value>
    public IEnumerable<IDataProvider<TDataService, TUnitOfWork>> Providers { get; }

    /// <summary>   Gets the resolve. </summary>
    /// <returns>   An IDataProvider&lt;TDataService,TUnitOfWork&gt; </returns>
    public IDataProvider<TDataService, TUnitOfWork> Resolve()
    {
        return Providers.Single(p => p.ProviderType == Options.ProviderType);
    }
}