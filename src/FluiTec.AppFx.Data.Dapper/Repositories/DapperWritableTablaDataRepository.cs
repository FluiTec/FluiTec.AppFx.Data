﻿using System;
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
/// A dapper writable tabla data repository.
/// </summary>
///
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
    /// Gets a value indicating whether the identity key.
    /// </summary>
    ///
    /// <value>
    /// True if identity key, false if not.
    /// </value>
    protected bool IdentityKey => KeyProperties.Any(kp => kp.ExtendedData.IdentityKey);

    #endregion

    #region IWritableTableDataRepository

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public virtual TEntity Add(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            UnitOfWork.Connection.InsertAuto(entity, UnitOfWork.Transaction);
            return entity;
        }

        UnitOfWork.Connection.Insert(entity, UnitOfWork.Transaction);
        return entity;
    }

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            await UnitOfWork.Connection.InsertAutoAsync(entity, UnitOfWork.Transaction, cancellationToken: ctx);
            return entity;
        }

        await UnitOfWork.Connection.InsertAsync(entity, UnitOfWork.Transaction, cancellationToken: ctx);
        return entity;
    }

    /// <summary>
    /// Adds a range of entities.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            UnitOfWork.Connection.InsertAutoMultiple(keyEntities, UnitOfWork.Transaction);
        }
        else
        {
            UnitOfWork.Connection.InsertMultiple(keyEntities, UnitOfWork.Transaction);
        }
    }

    /// <summary>
    /// Adds a range asynchronous.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// An asynchronous result.
    /// </returns>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (IdentityKey)
        {
            await UnitOfWork.Connection.InsertAutoMultipleAsync(keyEntities, UnitOfWork.Transaction, cancellationToken: ctx);
        }
        else
        {
            await UnitOfWork.Connection.InsertMultipleAsync(keyEntities, UnitOfWork.Transaction, cancellationToken: ctx);
        }
    }

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    ///
    /// <exception cref="UpdateException">  Thrown when an Update error condition occurs. </exception>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public virtual TEntity Update(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            if (UnitOfWork.Connection.Update(entity, originalTimeStamp, UnitOfWork.Transaction))
                return entity;
            throw new UpdateException(entity);
        }

        if (UnitOfWork.Connection.Update(entity, UnitOfWork.Transaction))
            return entity;
        throw new UpdateException(entity);
    }

    /// <summary>
    /// Updates the asynchronous described by entity.
    /// </summary>
    ///
    /// <exception cref="UpdateException">  Thrown when an Update error condition occurs. </exception>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The update.
    /// </returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            if (await UnitOfWork.Connection.UpdateAsync(entity, originalTimeStamp, UnitOfWork.Transaction))
                return entity;
            throw new UpdateException(entity);
        }

        if (await UnitOfWork.Connection.UpdateAsync(entity, UnitOfWork.Transaction, cancellationToken: ctx))
            return entity;
        throw new UpdateException(entity);
    }

    /// <summary>
    /// Deletes the given ID.
    /// </summary>
    ///
    /// <param name="keys"> The keys. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    public virtual bool Delete(params object[] keys)
    {
        return UnitOfWork.Connection.Delete<TEntity>(GetKey(keys), UnitOfWork.Transaction);
    }

    /// <summary>
    /// Deletes the given ID.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    public virtual bool Delete(TEntity entity)
    {
        return UnitOfWork.Connection.Delete<TEntity>(GetKey(entity), UnitOfWork.Transaction);
    }

    /// <summary>
    /// Deletes the asynchronous described by ID.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken ctx = default)
    {
        return await UnitOfWork.Connection.DeleteAsync<TEntity>(GetKey(entity), UnitOfWork.Transaction,
            cancellationToken: ctx);
    }

    /// <summary>
    /// Deletes the asynchronous described by ID.
    /// </summary>
    ///
    /// <param name="keys"> The keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    public virtual async Task<bool> DeleteAsync(object[] keys, CancellationToken ctx = default)
    {
        return await UnitOfWork.Connection.DeleteAsync<TEntity>(GetKey(keys), UnitOfWork.Transaction,
            cancellationToken: ctx);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets a key.
    /// </summary>
    ///
    /// <param name="entity">   The entity. </param>
    ///
    /// <returns>
    /// The key.
    /// </returns>
    protected IDictionary<string, object> GetKey(TEntity entity)
    {
        var key = new ExpandoObject() as IDictionary<string, object>;

        foreach (var k in KeyProperties)
        {
            key.Add(k.PropertyInfo.Name, k.PropertyInfo.GetValue(entity, null));
        }

        return key;
    }

    #endregion
}