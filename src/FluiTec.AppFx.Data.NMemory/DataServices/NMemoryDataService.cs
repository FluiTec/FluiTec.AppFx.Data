using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using NMemory;
using NMemory.Tables;
using NMemory.Utilities;

namespace FluiTec.AppFx.Data.NMemory.DataServices;

/// <summary>
///     A service for accessing memory data information.
/// </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class NMemoryDataService<TUnitOfWork> : DataService<TUnitOfWork>, INMemoryDataService
    where TUnitOfWork : NMemoryUnitOfWork, IUnitOfWork
{
    #region Constructors

    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="loggerFactory">    The logger factory. </param>
    /// <param name="nameService">      The name service. </param>
    protected NMemoryDataService(ILoggerFactory loggerFactory, IEntityNameService nameService) 
        : base(loggerFactory, nameService)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Database = ConfigureDatabase(new Database());
    }

    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="loggerFactory">    The logger factory. </param>
    protected NMemoryDataService(ILoggerFactory loggerFactory)
        : this(loggerFactory, EntityNameService.GetDefault())
    {
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Configure database.
    /// </summary>
    /// <param name="database"> The database. </param>
    protected abstract Database ConfigureDatabase(Database database);

    #endregion

    #region IDisposable

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    ///     resources.
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

    #region INMemoryDataService

    /// <summary>
    ///     Gets the database.
    /// </summary>
    /// <value>
    ///     The database.
    /// </value>
    public Database Database { get; }


    /// <summary>
    ///     Gets the table.
    /// </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <returns>
    ///     The table.
    /// </returns>
    public ITable<TEntity> GetTable<TEntity>() where TEntity : class
    {
        return Database.Tables.FindTable<TEntity>();
    }

    #endregion

    #region Migration

    /// <summary>
    ///     Gets a value indicating whether the supports migration.
    /// </summary>
    /// <value>
    ///     True if supports migration, false if not.
    /// </value>
    public override bool SupportsMigration => false;

    /// <summary>
    ///     Gets the migrator.
    /// </summary>
    /// <returns>
    ///     The migrator.
    /// </returns>
    public override IDataMigrator GetMigrator()
    {
        throw new NotImplementedException();
    }

    #endregion
}