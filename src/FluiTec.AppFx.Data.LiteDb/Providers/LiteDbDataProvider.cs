using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Providers;

/// <summary>   A lite database data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public class LiteDbDataProvider<TDataService, TUnitOfWork> : ILiteDbDataProvider,
    IDataProvider<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">  The data service. </param>
    /// <param name="options">      Options for controlling the operation. </param>
    protected LiteDbDataProvider(TDataService dataService, DataOptions options)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        NameStrategy = new DottedNameStrategy();
        if (options == null) throw new ArgumentNullException(nameof(options));
        PageSettings = new PageSettings(options.PageSize, options.MaxPageSize);
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">      The data service. </param>
    /// <param name="optionsMonitor">   The options monitor. </param>
    protected LiteDbDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        OptionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
        OptionsMonitor.OnChange(OnOptionsChanged);
        NameStrategy = new DottedNameStrategy();

        var options = OptionsMonitor.CurrentValue;
        PageSettings = new PageSettings(options.PageSize, options.MaxPageSize);
    }

    /// <summary>   Gets the options monitor. </summary>
    /// <value> The options monitor. </value>
    public IOptionsMonitor<DataOptions>? OptionsMonitor { get; }

    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    public TDataService DataService { get; }

    /// <summary>   Gets the name strategy. </summary>
    /// <value> The name strategy. </value>
    public INameStrategy NameStrategy { get; }

    /// <summary>   Gets the page settings. </summary>
    /// <value> The page settings. </value>
    public PageSettings PageSettings { get; private set; }

    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public ProviderType ProviderType => ProviderType.NMemory;

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public TUnitOfWork BeginUnitOfWork()
    {
        throw new NotImplementedException();
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        throw new NotImplementedException();
    }

    /// <summary>   Begins unitof work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public TUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork)
    {
        throw new NotImplementedException();
    }

    /// <summary>   Executes the 'options changed' action. </summary>
    /// <param name="arg1"> The first argument. </param>
    /// <param name="name"> The name. </param>
    private void OnOptionsChanged(DataOptions arg1, string? name)
    {
        var options = OptionsMonitor!.CurrentValue;
        PageSettings = new PageSettings(options.PageSize, options.MaxPageSize);
    }
}