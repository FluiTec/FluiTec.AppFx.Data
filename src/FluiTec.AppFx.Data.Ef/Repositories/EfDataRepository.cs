﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
///     An ef data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class EfDataRepository<TEntity> : ITableDataRepository<TEntity>, IExpressionDataRepository<TEntity>
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
    protected EfDataRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Logger = logger; // we accept null here
        EntityType = typeof(TEntity);
    }

    #endregion

    #region Properties

    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public EfUnitOfWork UnitOfWork { get; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IRepository> Logger { get; }

    /// <summary>	Gets the type of the entity. </summary>
    /// <value>	The type of the entity. </value>
    public Type EntityType { get; }

    /// <summary>
    ///     Gets the name of the table.
    /// </summary>
    /// <value>
    ///     The name of the table.
    /// </value>
    public string TableName => Set.EntityType.Name;

    /// <summary>
    ///     Gets the context.
    /// </summary>
    /// <value>
    ///     The context.
    /// </value>
    protected IDynamicDbContext Context => UnitOfWork.Context;

    /// <summary>
    ///     Gets the set.
    /// </summary>
    /// <value>
    ///     The set.
    /// </value>
    protected DbSet<TEntity> Set => UnitOfWork.Context.Set<TEntity>();

    #endregion

    #region ITableDataRepository

    /// <summary>
    ///     Gets an entity using the given identifier.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Get(params object[] keys)
    {
        return Set.Find(keys);
    }

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual async Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        return await Set.FindAsync(keys, ctx);
    }

    /// <summary>
    ///     Gets all entities in this collection.
    /// </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public virtual IEnumerable<TEntity> GetAll()
    {
        return Set.ToList();
    }

    /// <summary>
    ///     Gets all asynchronous.
    /// </summary>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ctx = default)
    {
        return await Set.ToListAsync(ctx);
    }

    /// <summary>
    /// Gets the paged in this collection.
    /// </summary>
    ///
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    ///
    /// <returns>
    /// An enumerator that allows foreach to be used to process the paged in this collection.
    /// </returns>
    public virtual IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize)
    {
        return Set.Skip(pageIndex*pageSize).Take(pageSize).ToList();
    }

    /// <summary>
    /// Gets the paged asynchronous in this collection.
    /// </summary>
    ///
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <param name="ctx">          (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// An enumerator that allows foreach to be used to process the paged asynchronous in this
    /// collection.
    /// </returns>
    public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ctx = default)
    {
        return await Set.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(ctx);
    }

    /// <summary>
    ///     Counts the number of records.
    /// </summary>
    /// <returns>
    ///     An int defining the total number of records.
    /// </returns>
    public virtual int Count()
    {
        return Set.Count();
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
        return Set.CountAsync(ctx);
    }

    #endregion

    #region IExpressionDataRepository

    /// <summary>
    ///     Enumerates the items in this collection that meet given criteria.
    /// </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the matched items.
    /// </returns>
    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
    {
        return Set.Where(expression).ToList();
    }

    /// <summary>
    ///     Finds the asynchronous in this collection.
    /// </summary>
    /// <param name="expression">   The expression. </param>
    /// <param name="ctx">          (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the asynchronous in this collection.
    /// </returns>
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken ctx = default)
    {
        return await Set.Where(expression).ToListAsync(ctx);
    }

    #endregion
}