using System;
using System.Collections.Concurrent;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.SequentialGuid;
using FluiTec.AppFx.Data.Sql;
using FluiTec.AppFx.Data.Sql.EventArgs;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Dapper.DataServices;

/// <summary>   A service for accessing base dapper data information. </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class BaseDapperDataService<TUnitOfWork> : DataService<TUnitOfWork>, IDapperDataService
    where TUnitOfWork : DapperUnitOfWork, IUnitOfWork
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly string _connectionString;

    #region Events

    /// <summary>
    ///     Event queue for all listeners interested in SqlGenerated events.
    /// </summary>
    public event EventHandler<SqlGeneratedEventArgs> SqlGenerated;

    #endregion

    #region ICommandCache

    /// <summary>   Gets from cache.</summary>
    /// <param name="repositoryType">   Type of the repository. </param>
    /// <param name="memberName">       Name of the member. </param>
    /// <param name="commandFunc">      The command function. </param>
    /// <returns>   The data that was read from the cache.</returns>
    public string GetFromCache(Type repositoryType, string memberName, Func<string> commandFunc)
    {
        Logger?.LogTrace("ICommandCache.GetFromCache({type}, {member})", repositoryType, memberName);
        var key = $"{repositoryType.FullName}.{memberName}";

        if (CommandCache.TryGetValue(key, out var result))
            return result;

        var cmd = commandFunc();
        CommandCache.TryAdd(key, cmd);
        return cmd;
    }

    #endregion

    #region IDisposable

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting
    ///     unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    ///     True to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        // nothing to do here
    }

    #endregion

    #region EventHandlers

    /// <summary>
    ///     Raises the SQL generated event.
    /// </summary>
    /// <param name="args"> Event information to send to registered event handlers. </param>
    protected virtual void OnSqlGenerated(SqlGeneratedEventArgs args)
    {
        Logger?.LogDebug("SqlBuilder generated statement for type '{type}': '{statement}'", args.Type,
            args.SqlStatement);
        SqlGenerated?.Invoke(this, args);
    }

    #endregion

    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    /// <param name="nameService">          The name service. </param>
    protected BaseDapperDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory,
        IEntityNameService nameService) :
        base(loggerFactory, nameService)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        if (SqlType.NeedsDateTimeMapping())
            DapperExtensions.InstallDateTimeOffsetMapper();
        if (dapperServiceOptions == null) throw new ArgumentNullException(nameof(dapperServiceOptions));

        _connectionString = dapperServiceOptions.ConnectionString;
        _connectionFactory = dapperServiceOptions.ConnectionFactory;
        Logger?.LogDebug("Initialized using static options.");

        CommandCache = new ConcurrentDictionary<string, string>();
        // ReSharper disable once VirtualMemberCallInConstructor
        SqlBuilder = SqlType.GetBuilder(nameService, loggerFactory);
        SqlBuilder.SqlGenerated += (sender, args) => OnSqlGenerated(args);
        // ReSharper disable once VirtualMemberCallInConstructor
        GuidGenerator = new CombSequentialGuidGenerator(SqlType);
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    protected BaseDapperDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) :
        this(dapperServiceOptions, loggerFactory, EntityNameService.GetDefault())
    {
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    /// <param name="nameService">          The name service. </param>
    protected BaseDapperDataService(IOptionsMonitor<IDapperServiceOptions> dapperServiceOptions,
        ILoggerFactory loggerFactory, IEntityNameService nameService) :
        base(loggerFactory, nameService)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        if (SqlType == SqlType.Mysql)
            DapperExtensions.InstallDateTimeOffsetMapper();
        if (dapperServiceOptions == null) throw new ArgumentNullException(nameof(dapperServiceOptions));
        if (dapperServiceOptions.CurrentValue == null)
            throw new ArgumentNullException(nameof(dapperServiceOptions));

        DapperServiceOptions = dapperServiceOptions;
        CommandCache = new ConcurrentDictionary<string, string>();
        Logger?.LogDebug("Initialized using dynamic options/IOptionsMonitor.");

        // ReSharper disable once VirtualMemberCallInConstructor
        SqlBuilder = SqlType.GetBuilder(nameService, loggerFactory);
        SqlBuilder.SqlGenerated += (sender, args) => OnSqlGenerated(args);
        // ReSharper disable once VirtualMemberCallInConstructor
        GuidGenerator = new CombSequentialGuidGenerator(SqlType);
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    protected BaseDapperDataService(IOptionsMonitor<IDapperServiceOptions> dapperServiceOptions,
        ILoggerFactory loggerFactory) :
        this(dapperServiceOptions, loggerFactory, EntityNameService.GetDefault())
    {
    }

    #endregion

    #region Properties

    /// <summary>   Gets options for controlling the dapper service. </summary>
    /// <value> Options that control the dapper service. </value>
    protected IOptionsMonitor<IDapperServiceOptions> DapperServiceOptions { get; }

    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    public IConnectionFactory ConnectionFactory =>
        _connectionFactory ?? DapperServiceOptions.CurrentValue.ConnectionFactory;

    /// <summary>   Gets the connection string. </summary>
    /// <value> The connection string. </value>
    public string ConnectionString => _connectionString ?? DapperServiceOptions.CurrentValue.ConnectionString;

    /// <summary>   Gets the command cache.</summary>
    /// <value> The command cache.</value>
    protected ConcurrentDictionary<string, string> CommandCache { get; }

    /// <summary>   Gets information describing the meta. </summary>
    /// <value> Information describing the meta. </value>
    public abstract IVersionTableMetaData MetaData { get; }

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public abstract string Schema { get; }

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public abstract SqlType SqlType { get; }

    /// <summary>
    ///     Gets the SQL builder.
    /// </summary>
    /// <value>
    ///     The SQL builder.
    /// </value>
    public ISqlBuilder SqlBuilder { get; }

    /// <summary>
    /// Gets the unique identifier generator.
    /// </summary>
    ///
    /// <value>
    /// The unique identifier generator.
    /// </value>
    public override ISequentialGuidGenerator GuidGenerator { get; }

    #endregion
}