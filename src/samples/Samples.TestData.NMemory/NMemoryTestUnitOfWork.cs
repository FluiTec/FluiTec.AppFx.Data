using System.Transactions;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Samples.TestData.DataServices;
using Samples.TestData.Entities;
using Samples.TestData.UnitsOfWork;

namespace Samples.TestData.NMemory;

/// <summary>   A memory test unit of work. </summary>
public class NMemoryTestUnitOfWork : NMemoryUnitOfWork, ITestUnitOfWork
{
    /// <summary>   The dummy repository. </summary>
    private Lazy<IWritableTableDataRepository<DummyEntity>> _dummyRepository = null!;

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dataService">          The data service. </param>
    /// <param name="dataProvider">         The data provider. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    public NMemoryTestUnitOfWork(ITestDataService dataService, INMemoryDataProvider dataProvider,
        TransactionOptions transactionOptions)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), transactionOptions)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        InitializeRepositories();
    }

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dataService">      The data service. </param>
    /// <param name="dataProvider">     The data provider. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    public NMemoryTestUnitOfWork(ITestDataService dataService, INMemoryDataProvider dataProvider,
        IUnitOfWork parentUnitOfWork)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), parentUnitOfWork)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        InitializeRepositories();
    }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public INMemoryDataProvider DataProvider { get; }

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
                new NMemoryWritableTableRepository<DummyEntity>(DataService, DataProvider, this));
    }
}