using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.UnitsOfWork;
using System.Transactions;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using NMemory;
using NMemory.Tables;
using NMemory.Utilities;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;

/// <summary>   A memory test data provider. </summary>
public class NMemoryTestDataProvider : NMemoryDataProvider<ITestDataService, ITestUnitOfWork>, ITestDataProvider
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    public NMemoryTestDataProvider(ITestDataService dataService) : base(dataService)
    {
    }

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork()
    {
        return new NMemoryTestUnitOfWork(DataService, this, new TransactionOptions());
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        return new NMemoryTestUnitOfWork(DataService, this, transactionOptions);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork)
    {
        return new NMemoryTestUnitOfWork(DataService, this, parentUnitOfWork);
    }

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected override Database ConfigureDatabase()
    {
        var db = new Database();
        db.Tables.Create(e => e.Id, new IdentitySpecification<DummyEntity>(e => e.Id));

        var t = db.Tables.FindTable<DummyEntity>();
        t.Insert(new DummyEntity { Id = 1, Name = "Test 1" });
        t.Insert(new DummyEntity { Id = 2, Name = "Test 2" });

        return db;
    }
}