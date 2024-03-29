﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using ImmediateReflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
///     An ef writable data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class EfWritableTableDataRepository<TEntity> : EfDataRepository<TEntity>, IWritableTableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected EfWritableTableDataRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
        logger)
    {
    }

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

        foreach (var k in SqlCache.TypeKeyPropertiesCache(EntityType)
                     .OrderBy(kp => kp.ExtendedData.Order)
                     .ToList())
            key.Add(k.PropertyInfo.Name, k.PropertyInfo.GetValue(entity));

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
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        Set.Add(entity);
        Context.SaveChanges();
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
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        var result = await Set.AddAsync(entity, ctx);
        Context.SaveChanges();
        return result.Entity;
    }

    /// <summary>
    ///     Adds a range of entities.
    /// </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        Set.AddRange(keyEntities);
        Context.SaveChanges();
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
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        await Set.AddRangeAsync(keyEntities, ctx);
        Context.SaveChanges();
    }

    /// <summary>
    ///     Updates the given entity.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Update(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            var dbEntity = Get(GetKey(entity)) as ITimeStampedKeyEntity;
            if (dbEntity?.TimeStamp != originalTimeStamp)
                throw new UpdateException(entity);
        }

        Set.Attach(entity);
        var entry = UnitOfWork.Context.Entry(entity);
        if (entry.State == EntityState.Added)
            throw new UpdateException(entity);
        UnitOfWork.Context.Entry(entity).State = EntityState.Modified;

        try
        {
            Context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new UpdateException(entity);
        }

        return entity;
    }

    /// <summary>
    ///     Updates the asynchronous described by entity.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The update.
    /// </returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            var dbEntity = await GetAsync(GetKey(entity).Values.ToArray(), ctx) as ITimeStampedKeyEntity;
            if (dbEntity?.TimeStamp != originalTimeStamp)
                throw new UpdateException(entity);
        }

        Set.Attach(entity);
        if (UnitOfWork.Context.Entry(entity).State == EntityState.Added)
            throw new UpdateException(entity);
        UnitOfWork.Context.Entry(entity).State = EntityState.Modified;

        try
        {
            Context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new UpdateException(entity);
        }

        return entity;
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
        return Delete(Get(keys));
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
        var entry = Set.Remove(entity);
        Context.SaveChanges();
        return entry.State == EntityState.Detached;
    }

    /// <summary>
    ///     Deletes the asynchronous described by ID.
    /// </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The delete.
    /// </returns>
    public virtual Task<bool> DeleteAsync(TEntity entity, CancellationToken ctx = default)
    {
        var entry = Set.Remove(entity);
        Context.SaveChanges();
        return Task.FromResult(entry.State == EntityState.Detached);
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
        var entity = await GetAsync(keys, ctx);
        return await DeleteAsync(entity, ctx);
    }

    #endregion
}