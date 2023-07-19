using System.Transactions;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers.Fixtures;

/// <summary>   A lite database test unit of work. </summary>
public class LiteDbTestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
{
    /// <summary>   The dummy repository. </summary>
    private IRepository<DummyEntity>? _dummyRepository;

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">          The data service. </param>
    /// <param name="dataProvider">         The data provider. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <param name="database">             The database. </param>
    public LiteDbTestUnitOfWork(ITestDataService dataService, ILiteDbDataProvider dataProvider,
        TransactionOptions transactionOptions, LiteDatabase database)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), transactionOptions, database)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">      The data service. </param>
    /// <param name="dataProvider">     The data provider. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="database">         The database. </param>
    public LiteDbTestUnitOfWork(ITestDataService dataService, ILiteDbDataProvider dataProvider,
        IUnitOfWork parentUnitOfWork, LiteDatabase database)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), parentUnitOfWork, database)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public ILiteDbDataProvider DataProvider { get; }

    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    public ITestDataService DataService { get; }

    /// <summary>   Gets the dummy repository. </summary>
    /// <value> The dummy repository. </value>
    public IRepository<DummyEntity> DummyRepository
    {
        get
        {
            if (_dummyRepository != null)
                return _dummyRepository!;
            _dummyRepository = new LiteDbRepository<DummyEntity>(DataService, DataProvider);
            return _dummyRepository;
        }
    }
}