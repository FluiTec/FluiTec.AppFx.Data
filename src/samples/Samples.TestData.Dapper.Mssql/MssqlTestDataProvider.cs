using System.Transactions;
using FluiTec.AppFx.Data.Dapper.Mssql.Providers;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;
using Samples.TestData.DataServices;
using Samples.TestData.UnitsOfWork;

namespace Samples.TestData.Dapper.Mssql;

/// <summary>   A mssql test data provider. </summary>
public class MssqlTestDataProvider : MssqlDapperDataProvider<ITestDataService, ITestUnitOfWork>,
    IDataProvider<ITestDataService, ITestUnitOfWork>
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">              The data service. </param>
    /// <param name="options">                  Options for controlling the operation. </param>
    /// <param name="connectionStringOptions">  Options for controlling the connection string. </param>
    public MssqlTestDataProvider(ITestDataService dataService, DataOptions<ITestDataService> options, ConnectionStringOptions<ITestDataService> connectionStringOptions) 
        : base(dataService, options, connectionStringOptions)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <param name="dataService">                      The data service. </param>
    /// <param name="optionsMonitor">                   The options monitor. </param>
    /// <param name="connectionStringOptionsMonitor">   The connection string options monitor. </param>
    public MssqlTestDataProvider(ITestDataService dataService, IOptionsMonitor<DataOptions<ITestDataService>> optionsMonitor, IOptionsMonitor<ConnectionStringOptions<ITestDataService>> connectionStringOptionsMonitor) 
        : base(dataService, optionsMonitor, connectionStringOptionsMonitor)
    {
    }

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork()
    {
        return new DapperTestUnitOfWork(new TransactionOptions(),  DataService, this);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        return new DapperTestUnitOfWork(transactionOptions, DataService, this);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(IUnitOfWork parentUnitOfWork)
    {
        return new DapperTestUnitOfWork(parentUnitOfWork, DataService, this);
    }
}