using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;
using NMemory;
using NMemory.Tables;
using NMemory.Utilities;

namespace FluiTec.AppFx.Data.NMemory.Providers;

/// <summary>   A memory data provider. </summary>
public abstract class NMemoryDataProvider<TDataService, TUnitOfWork> : INMemoryDataProvider,
    IDataProvider<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   The database. </summary>
    private Database? _database;

    /// <summary>   Specialized default constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dataService">  The data service. </param>
    /// <param name="options">      Options for controlling the operation. </param>
    protected NMemoryDataProvider(TDataService dataService, DataOptions options)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        NameStrategy = new DottedNameStrategy();
        if (options == null) throw new ArgumentNullException(nameof(options));
        PageSettings = new PageSettings(options.PageSize, options.MaxPageSize);
    }

    /// <summary>   Specialized default constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dataService">      The data service. </param>
    /// <param name="optionsMonitor">   The options monitor. </param>
    protected NMemoryDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor)
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

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public abstract TUnitOfWork BeginUnitOfWork();

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public abstract TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions);

    /// <summary>   Begins unit of work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public abstract TUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork);

    /// <summary>   Gets the name strategy. </summary>
    /// <value> The name strategy. </value>
    public INameStrategy NameStrategy { get; }

    /// <summary>   Gets the page settings. </summary>
    /// <value> The page settings. </value>
    public PageSettings PageSettings { get; private set; }

    /// <summary>   Gets the database. </summary>
    /// <value> The database. </value>
    public virtual Database Database
    {
        get
        {
            if (_database != null) return _database;
            _database = ConfigureDatabase();
            return _database;
        }
    }

    /// <summary>   Gets the table. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <returns>   The table. </returns>
    public ITable<TEntity> GetTable<TEntity>() where TEntity : class
    {
        return Database.Tables.FindTable<TEntity>();
    }

    /// <summary>   Executes the 'options changed' action. </summary>
    /// <param name="arg1"> The first argument. </param>
    /// <param name="name"> The name. </param>
    private void OnOptionsChanged(DataOptions arg1, string? name)
    {
        var options = OptionsMonitor!.CurrentValue;
        PageSettings = new PageSettings(options.PageSize, options.MaxPageSize);
    }

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected abstract Database ConfigureDatabase();
}