using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Providers;

/// <summary>   A lite database data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class LiteDbDataProvider<TDataService, TUnitOfWork> : BaseDataProvider<TDataService, TUnitOfWork>, ILiteDbDataProvider
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary> The database.</summary>
    private LiteDatabase? _database;

    /// <summary> Constructor.</summary>
    /// <param name="dataService"> The data service. </param>
    /// <param name="options">     Options for controlling the operation. </param>
    protected LiteDbDataProvider(TDataService dataService, DataOptions options) : base(dataService, options)
    {
    }

    /// <summary> Constructor.</summary>
    /// <param name="dataService">    The data service. </param>
    /// <param name="optionsMonitor"> The options monitor. </param>
    protected LiteDbDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor) : base(dataService, optionsMonitor)
    {
    }

    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public override ProviderType ProviderType => ProviderType.NMemory;
    
    /// <summary> Gets the database.</summary>
    /// <value> The database.</value>
    public virtual LiteDatabase Database
    {
        get
        {
            if (_database != null) return _database;
            _database = ConfigureDatabase();
            return _database;
        }
    }

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected abstract LiteDatabase ConfigureDatabase();
}