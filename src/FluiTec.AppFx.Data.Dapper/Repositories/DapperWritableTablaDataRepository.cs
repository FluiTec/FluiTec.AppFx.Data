using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>
///     A dapper writable tabla data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class DapperWritableTablaDataRepository<TEntity> : DapperTableDataRepository<TEntity>,
    IWritableTableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperWritableTablaDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Gets a value indicating whether the identity key.
    /// </summary>
    /// <value>
    ///     True if identity key, false if not.
    /// </value>
    protected bool IdentityKey => KeyProperties.Any(kp => kp.ExtendedData.IdentityKey);

    #endregion

    #region Methods

    /// <summary>
    ///     Gets a key.
    /// </summary>
    /// <param name="entity">   The entity. </param>
    /// <returns>
    ///     The key.
    /// </returns>
    protected IDictionary<string, object> GetKey(TEntity entity)
    {
        var key = new ExpandoObject() as IDictionary<string, object>;

        foreach (var k in KeyProperties) key.Add(k.PropertyInfo.Name, k.PropertyInfo.GetValue(entity));

        return key;
    }

    #endregion

    #region IWritableTableDataRepository

    /// <summary>
    ///     Adds entity.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Add(TEntity entity)
    {
        Logger?.LogTrace("Add<{type}>({entity})", typeof(TEntity), entity);

        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            UnitOfWork.Connection.InsertAuto(SqlBuilder, entity, UnitOfWork.Transaction);
            return entity;
        }

        UnitOfWork.Connection.Insert(SqlBuilder, entity, UnitOfWork.Transaction);
        return entity;
    }

    /// <summary>
    ///     Adds entity.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default)
    {
        Logger?.LogTrace("AddAsync<{type}>({entity})", typeof(TEntity), entity);

        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            await UnitOfWork.Connection.InsertAutoAsync(SqlBuilder, entity, UnitOfWork.Transaction,
                cancellationToken: ctx);
            return entity;
        }

        await UnitOfWork.Connection.InsertAsync(SqlBuilder, entity, UnitOfWork.Transaction, cancellationToken: ctx);
        return entity;
    }

    /// <summary>
    ///     Adds a range of entities.
    /// </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        Logger?.LogTrace("AddRange<{type}>", typeof(TEntity));

        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
            UnitOfWork.Connection.InsertAutoMultiple(SqlBuilder, keyEntities, UnitOfWork.Transaction);
        else
            UnitOfWork.Connection.InsertMultiple(SqlBuilder, keyEntities, UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Adds a range asynchronous.
    /// </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An asynchronous result.
    /// </returns>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default)
    {
        Logger?.LogTrace("AddRangeAsync<{type}>", typeof(TEntity));

        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
            await UnitOfWork.Connection.InsertAutoMultipleAsync(SqlBuilder, keyEntities, UnitOfWork.Transaction,
                cancellationToken: ctx);
        else
            await UnitOfWork.Connection.InsertMultipleAsync(SqlBuilder, keyEntities, UnitOfWork.Transaction,
                cancellationToken: ctx);
    }

    /// <summary>
    ///     Updates the given entity.
    /// </summary>
    /// <exception cref="UpdateException">  Thrown when an Update error condition occurs. </exception>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Update(TEntity entity)
    {
        Logger?.LogTrace("Update<{type}>({entity})", typeof(TEntity), entity);

        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            if (UnitOfWork.Connection.Update(SqlBuilder, entity, originalTimeStamp, UnitOfWork.Transaction))
                return entity;
            throw new UpdateException(entity);
        }

        if (UnitOfWork.Connection.Update(SqlBuilder, entity, UnitOfWork.Transaction))
            return entity;
        throw new UpdateException(entity);
    }

    /// <summary>
    ///     Updates the asynchronous described by entity.
    /// </summary>
    /// <exception cref="UpdateException">  Thrown when an Update error condition occurs. </exception>
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The update.
    /// </returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default)
    {
        Logger?.LogTrace("UpdateAsync<{type}>({entity})", typeof(TEntity), entity);

        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            if (await UnitOfWork.Connection.UpdateAsync(SqlBuilder, entity, originalTimeStamp, UnitOfWork.Transaction))
                return entity;
            throw new UpdateException(entity);
        }

        if (await UnitOfWork.Connection.UpdateAsync(SqlBuilder, entity, UnitOfWork.Transaction, cancellationToken: ctx))
            return entity;
        throw new UpdateException(entity);
    }

    /// <summary>
    ///     Deletes the given ID.
    /// </summary>
    /// <param name="keys"> The keys. </param>
    /// <returns>
    ///     True if it succeeds, false if it fails.
    /// </returns>
    public virtual bool Delete(params object[] keys)
    {
        Logger?.LogTrace("Delete<{type}>({keys})", typeof(TEntity), keys);
        return UnitOfWork.Connection.Delete<TEntity>(SqlBuilder, GetKey(keys), UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Deletes the given ID.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>
    ///     True if it succeeds, false if it fails.
    /// </returns>
    public virtual bool Delete(TEntity entity)
    {
        Logger?.LogTrace("Delete<{type}>({entity})", typeof(TEntity), entity);
        return UnitOfWork.Connection.Delete<TEntity>(SqlBuilder, GetKey(entity), UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Deletes the asynchronous described by ID.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The delete.
    /// </returns>
    public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken ctx = default)
    {
        Logger?.LogTrace("DeleteAsync<{type}>({entity})", typeof(TEntity), entity);
        return await UnitOfWork.Connection.DeleteAsync<TEntity>(SqlBuilder, GetKey(entity), UnitOfWork.Transaction,
            cancellationToken: ctx);
    }

    /// <summary>
    ///     Deletes the asynchronous described by ID.
    /// </summary>
    /// <param name="keys"> The keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The delete.
    /// </returns>
    public virtual async Task<bool> DeleteAsync(object[] keys, CancellationToken ctx = default)
    {
        Logger?.LogTrace("DeleteAsync<{type}>({keys})", typeof(TEntity), keys);
        return await UnitOfWork.Connection.DeleteAsync<TEntity>(SqlBuilder, GetKey(keys), UnitOfWork.Transaction,
            cancellationToken: ctx);
    }

    #endregion
}