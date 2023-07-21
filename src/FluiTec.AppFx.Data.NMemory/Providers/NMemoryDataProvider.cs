using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;
using NMemory;
using NMemory.Tables;
using NMemory.Utilities;

namespace FluiTec.AppFx.Data.NMemory.Providers;

/// <summary>   A memory data provider. </summary>
public abstract class NMemoryDataProvider<TDataService, TUnitOfWork> : BaseDataProvider<TDataService, TUnitOfWork>,
    INMemoryDataProvider
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   The database. </summary>
    private Database? _database;

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="options">      Options for controlling the operation. </param>
    protected NMemoryDataProvider(TDataService dataService, DataOptions options) : base(dataService, options)
    {
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">      The data service. </param>
    /// <param name="optionsMonitor">   The options monitor. </param>
    protected NMemoryDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor) : base(
        dataService, optionsMonitor)
    {
    }

    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public override ProviderType ProviderType => ProviderType.NMemory;

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