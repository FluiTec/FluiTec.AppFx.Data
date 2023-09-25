using System;
using System.Linq;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.StatementProviders;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Dapper.Providers;

/// <summary>   A dapper data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class DapperDataProvider<TDataService, TUnitOfWork> : BaseDataProvider<TDataService, TUnitOfWork>,
    IDapperDataProvider
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">              The data service. </param>
    /// <param name="options">                  Options for controlling the operation. </param>
    /// <param name="connectionStringOptions">  Options for controlling the connection string. </param>
    protected DapperDataProvider(TDataService dataService, DataOptions options, ConnectionStringOptions<TDataService> connectionStringOptions) : base(dataService, options)
    {
        if (connectionStringOptions == null) throw new ArgumentNullException(nameof(connectionStringOptions));
        ConnectionString = connectionStringOptions.Single(cso => cso.Key == SqlType.ToString()).Value;
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">                      The data service. </param>
    /// <param name="optionsMonitor">                   The options monitor. </param>
    /// <param name="connectionStringOptionsMonitor">   The connection string options monitor. </param>
    protected DapperDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor, IOptionsMonitor<ConnectionStringOptions<TDataService>> connectionStringOptionsMonitor) : base(dataService, optionsMonitor)
    {
        if (connectionStringOptionsMonitor == null) throw new ArgumentNullException(nameof(connectionStringOptionsMonitor));
        connectionStringOptionsMonitor.OnChange(o =>
            ConnectionString = o.Single(cso => cso.Key == SqlType.ToString()).Value);
        ConnectionString = connectionStringOptionsMonitor.CurrentValue.Single(cso => cso.Key == SqlType.ToString()).Value;
    }

    /// <summary>   Gets or sets the connection string. </summary>
    /// <value> The connection string. </value>
    public string ConnectionString { get; protected set; }

    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    public abstract IConnectionFactory ConnectionFactory { get; }

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public abstract SqlType SqlType { get; }

    /// <summary>   Gets the statement provider. </summary>
    /// <value> The statement provider. </value>
    public abstract IStatementProvider StatementProvider { get; }
}