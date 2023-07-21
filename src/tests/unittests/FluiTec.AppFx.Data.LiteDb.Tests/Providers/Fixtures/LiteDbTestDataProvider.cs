using System.Transactions;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers.Fixtures;

/// <summary>   A memory test data provider. </summary>
public class LiteDbTestDataProvider : LiteDbDataProvider<ITestDataService, ITestUnitOfWork>, ITestDataProvider
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="options">      Options for controlling the operation. </param>
    public LiteDbTestDataProvider(ITestDataService dataService, DataOptions options) : base(dataService, options)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <param name="dataService">      The data service. </param>
    /// <param name="optionsMonitor">   The options monitor. </param>
    public LiteDbTestDataProvider(ITestDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor) : base(
        dataService, optionsMonitor)
    {
    }

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork()
    {
        return new LiteDbTestUnitOfWork(DataService, this, new TransactionOptions(), Database);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        return new LiteDbTestUnitOfWork(DataService, this, transactionOptions, Database);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(IUnitOfWork parentUnitOfWork)
    {
        return new LiteDbTestUnitOfWork(DataService, this, parentUnitOfWork, Database);
    }

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected override LiteDatabase ConfigureDatabase()
    {
        var db = new LiteDatabase("datab.db");
        return db;
    }
}