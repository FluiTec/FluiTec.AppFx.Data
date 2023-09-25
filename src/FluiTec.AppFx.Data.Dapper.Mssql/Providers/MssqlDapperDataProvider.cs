using System;
using FluiTec.AppFx.Data.Dapper.Providers;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.StatementBuilders;
using FluiTec.AppFx.Data.Sql.StatementProviders;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Dapper.Mssql.Providers;

/// <summary>   A mssql dapper data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class MssqlDapperDataProvider<TDataService, TUnitOfWork> : DapperDataProvider<TDataService, TUnitOfWork>,
    IDapperDataProvider
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">              The data service. </param>
    /// <param name="options">                  Options for controlling the operation. </param>
    /// <param name="connectionStringOptions">  Options for controlling the connection string. </param>
    protected MssqlDapperDataProvider(TDataService dataService, DataOptions options, ConnectionStringOptions2<TDataService> connectionStringOptions) 
        : base(dataService, options, connectionStringOptions)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="dataService">                      The data service. </param>
    /// <param name="optionsMonitor">                   The options monitor. </param>
    /// <param name="connectionStringOptionsMonitor">   The connection string options monitor. </param>
    protected MssqlDapperDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor, IOptionsMonitor<ConnectionStringOptions2<TDataService>> connectionStringOptionsMonitor) 
        : base(dataService, optionsMonitor, connectionStringOptionsMonitor)
    {
    }
    
    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public override ProviderType ProviderType => ProviderType.Mssql;

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public override SqlType SqlType => SqlType.Mssql;

    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    public override IConnectionFactory ConnectionFactory { get; } = new MssqlConnectionFactory();
    
    /// <summary>   Gets the statement provider. </summary>
    /// <value> The statement provider. </value>
    public override IStatementProvider StatementProvider { get; } = new CachingStatementProvider(new MicrosoftSqlStatementBuilder());
}