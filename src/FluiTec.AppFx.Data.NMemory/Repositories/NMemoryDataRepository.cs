using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>
///     A memory data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class NMemoryDataRepository<TEntity> : IDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected NMemoryDataRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Logger = logger; // we accept null here
        EntityType = typeof(TEntity);
        TableName = GetTableName();
        Table = UnitOfWork.NMemoryDataService.GetTable<TEntity>();
    }

    #endregion

    #region Properties

    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public NMemoryUnitOfWork UnitOfWork { get; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IRepository> Logger { get; }

    /// <summary>	Gets the type of the entity. </summary>
    /// <value>	The type of the entity. </value>
    public Type EntityType { get; }

    /// <summary>   Gets the name of the table. </summary>
    /// <value> The name of the table. </value>
    public virtual string TableName { get; }

    /// <summary>
    ///     Gets the table.
    /// </summary>
    /// <value>
    ///     The table.
    /// </value>
    public ITable<TEntity> Table { get; }

    #endregion

    #region Methods

    /// <summary>   Gets table name. </summary>
    /// <returns>   The table name. </returns>
    protected string GetTableName()
    {
        return GetTableName(EntityType);
    }

    /// <summary>	Gets table name. </summary>
    /// <returns>	The table name. </returns>
    protected string GetTableName(Type t)
    {
        return t.Name;
    }

    #endregion

    #region IDataRepository

    /// <summary>
    ///     Gets all entities in this collection.
    /// </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public IEnumerable<TEntity> GetAll()
    {
        return Table.ToList();
    }

    /// <summary>
    ///     Gets all asynchronous.
    /// </summary>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ctx = default)
    {
        return Task.FromResult(GetAll());
    }

    /// <summary>
    ///     Counts the number of records.
    /// </summary>
    /// <returns>
    ///     An int defining the total number of records.
    /// </returns>
    public int Count()
    {
        return (int) Table.Count;
    }

    /// <summary>
    ///     Count asynchronous.
    /// </summary>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The count.
    /// </returns>
    public Task<int> CountAsync(CancellationToken ctx = default)
    {
        return Task.FromResult(Count());
    }

    #endregion
}