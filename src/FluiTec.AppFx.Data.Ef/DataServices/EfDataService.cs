using System;
using System.Collections.Generic;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.Ef.DataServices;

/// <summary>
///     A service for accessing ef data information.
/// </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class EfDataService<TUnitOfWork> : BaseEfDataService<TUnitOfWork>
    where TUnitOfWork : EfUnitOfWork, IUnitOfWork
{
    #region Fields

    private IVersionTableMetaData _metaData;

    #endregion

    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    protected EfDataService(ISqlServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="options">              Options for controlling the operation. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    /// <param name="entityNameService">    The entity name service. </param>
    protected EfDataService(ISqlServiceOptions options, ILoggerFactory loggerFactory,
        IEntityNameService entityNameService)
        : base(options, loggerFactory, entityNameService)
    {
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    protected EfDataService(IOptionsMonitor<ISqlServiceOptions> options, ILoggerFactory loggerFactory)
        : base(options, loggerFactory)
    {
    }

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="options">              Options for controlling the operation. </param>
    /// <param name="loggerFactory">        The logger factory. </param>
    /// <param name="entityNameService">    The entity name service. </param>
    protected EfDataService(IOptionsMonitor<ISqlServiceOptions> options, ILoggerFactory loggerFactory,
        IEntityNameService entityNameService)
        : base(options, loggerFactory, entityNameService)
    {
    }

    #endregion

    #region Migration

    /// <summary>   Gets information describing the meta. </summary>
    /// <value> Information describing the meta. </value>
    public IVersionTableMetaData MetaData => _metaData ??= new VersionTable(Schema, SupportsSchema());

    /// <summary>   Gets a value indicating whether the supports migration. </summary>
    /// <value> True if supports migration, false if not. </value>
    public override bool SupportsMigration => MetaData != null;

    /// <summary>   Configure SQL type. </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside
    ///     the required range.
    /// </exception>
    /// <returns>   An Action&lt;IMigrationRunnerBuilder&gt; </returns>
    protected virtual Action<IMigrationRunnerBuilder> ConfigureSqlType()
    {
        return SqlType switch
        {
            SqlType.Mssql => rb => rb.AddSqlServer2016(),
            SqlType.Mysql => rb => rb.AddMySql5(),
            SqlType.Pgsql => rb => rb.AddPostgres(),
            SqlType.Sqlite => rb => rb.AddSQLite(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>   Determines if we can supports schema.</summary>
    /// <returns>   True if it succeeds, false if it fails.</returns>
    protected virtual bool SupportsSchema()
    {
        return SqlType switch
        {
            SqlType.Mysql => false,
            _ => true
        };
    }

    /// <summary>   Gets the migration assemblies in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the migration assemblies in this
    ///     collection.
    /// </returns>
    /// <remarks>
    ///     Returns the assembly containing the implementing dataservice by default.
    /// </remarks>
    protected virtual IEnumerable<Assembly> GetMigrationAssemblies()
    {
        return new[] {GetType().BaseType?.Assembly, GetType().Assembly};
    }

    /// <summary>   Gets the migrator. </summary>
    /// <returns>   The migrator. </returns>
    public override IDataMigrator GetMigrator()
    {
        return new DataMigrator(ConnectionString, GetMigrationAssemblies(), MetaData, ConfigureSqlType());
    }

    #endregion
}