using System.Transactions;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers.Fixtures;

/// <summary>   A configurable n memory test data provider. </summary>
public class ConfigurableLiteDbTestDataProvider : ConfigurableLiteDbDataProvider<ITestDataService, ITestUnitOfWork>,
    ITestDataProvider
{
    /// <summary>   Constructor. </summary>
    /// <param name="configureDelegate">    The configure delegate. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="options">              Options for controlling the operation. </param>
    public ConfigurableLiteDbTestDataProvider(Func<LiteDatabase> configureDelegate, ITestDataService dataService,
        DataOptions options)
        : base(configureDelegate, dataService, options)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <param name="configureDelegate">    The configure delegate. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="optionsMonitor">       The options monitor. </param>
    public ConfigurableLiteDbTestDataProvider(Func<LiteDatabase> configureDelegate, ITestDataService dataService,
        IOptionsMonitor<DataOptions> optionsMonitor)
        : base(configureDelegate, dataService, optionsMonitor)
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
}