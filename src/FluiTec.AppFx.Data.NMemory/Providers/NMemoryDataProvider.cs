using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.UnitsOfWork;
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
    /// <param name="dataService">  The data service. </param>
    protected NMemoryDataProvider(TDataService dataService)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        NameStrategy = new DottedNameStrategy();
    }

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

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected abstract Database ConfigureDatabase();
}