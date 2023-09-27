using System.Transactions;
using FluiTec.AppFx.Data.Dapper.Providers;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Samples.TestData.DataServices;
using Samples.TestData.Entities;
using Samples.TestData.UnitsOfWork;

namespace Samples.TestData.Dapper;

/// <summary>   A dapper test unit of work. </summary>
public class DapperTestUnitOfWork : DapperUnitOfWork, ITestUnitOfWork
{
    /// <summary>   The dummy repository. </summary>
    private Lazy<IWritableTableDataRepository<DummyEntity>> _dummyRepository = null!;

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="dataProvider">         The data provider. </param>
    public DapperTestUnitOfWork(TransactionOptions transactionOptions, ITestDataService dataService,
        IDapperDataProvider dataProvider)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), transactionOptions, dataProvider)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        InitializeRepositories();
    }

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="dataService">      The data service. </param>
    /// <param name="dataProvider">     The data provider. </param>
    public DapperTestUnitOfWork(IUnitOfWork parentUnitOfWork, ITestDataService dataService,
        IDapperDataProvider dataProvider)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), parentUnitOfWork, dataProvider)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        InitializeRepositories();
    }

    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    public ITestDataService DataService { get; }

    /// <summary>   Gets the dummy repository. </summary>
    /// <value> The dummy repository. </value>
    public IWritableTableDataRepository<DummyEntity> DummyRepository => _dummyRepository.Value;

    /// <summary>   Initializes the repositories. </summary>
    protected void InitializeRepositories()
    {
        _dummyRepository =
            new Lazy<IWritableTableDataRepository<DummyEntity>>(() =>
                new DapperWritableTableRepository<DummyEntity>(DataService, DataProvider, this));
    }
}