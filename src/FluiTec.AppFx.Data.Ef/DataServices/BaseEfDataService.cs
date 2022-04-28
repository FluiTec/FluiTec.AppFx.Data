using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Ef.DataServices;

/// <summary>
///     A service for accessing base ef data information.
/// </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class BaseEfDataService<TUnitOfWork> : DataService<TUnitOfWork>, IEfDataService
    where TUnitOfWork : EfUnitOfWork, IUnitOfWork
{
    private readonly string _connectionString;
    private readonly SqlType? _sqlType;

    #region IEfDataService

    /// <summary>
    ///     Gets the context.
    /// </summary>
    /// <returns>
    ///     The context.
    /// </returns>
    public abstract IDynamicDbContext GetContext();

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

    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    protected BaseEfDataService(ISqlServiceOptions options, ILoggerFactory loggerFactory) : base(loggerFactory)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));
        _connectionString = options.ConnectionString;
        _sqlType = options.SqlType;
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    protected BaseEfDataService(IOptionsMonitor<ISqlServiceOptions> options, ILoggerFactory loggerFactory) :
        base(loggerFactory)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (options.CurrentValue == null)
            throw new ArgumentNullException(nameof(options));
        ServiceOptions = options;
    }

    #endregion

    #region Properties

    /// <summary>   Gets options for controlling the dapper service. </summary>
    /// <value> Options that control the dapper service. </value>
    protected IOptionsMonitor<ISqlServiceOptions> ServiceOptions { get; }

    public string ConnectionString => _connectionString ?? ServiceOptions.CurrentValue.ConnectionString;

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public SqlType SqlType => _sqlType ?? ServiceOptions.CurrentValue.SqlType;

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public abstract string Schema { get; }

    #endregion
}