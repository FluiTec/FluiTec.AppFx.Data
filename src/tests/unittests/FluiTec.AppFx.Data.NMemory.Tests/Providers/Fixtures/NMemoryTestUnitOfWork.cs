using System.Transactions;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;

public class NMemoryTestUnitOfWork : NMemoryUnitOfWork, ITestUnitOfWork
{
    private IRepository<DummyEntity>? _dummyRepository;

    public NMemoryTestUnitOfWork(ITestDataService dataService, INMemoryDataProvider dataProvider,
        TransactionOptions transactionOptions)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), transactionOptions)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public NMemoryTestUnitOfWork(ITestDataService dataService, INMemoryDataProvider dataProvider,
        IUnitOfWork parentUnitOfWork)
        : base(dataService.LoggerFactory?.CreateLogger<IUnitOfWork>(), parentUnitOfWork)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public INMemoryDataProvider DataProvider { get; }

    public ITestDataService DataService { get; }

    public IRepository<DummyEntity> DummyRepository
    {
        get
        {
            if (_dummyRepository != null)
                return _dummyRepository!;
            _dummyRepository = new NMemoryRepository<DummyEntity>(DataService, DataProvider);
            return _dummyRepository;
        }
    }
}