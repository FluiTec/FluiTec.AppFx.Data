using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

public abstract class NMemoryWritableKeyTableDataRepository<TEntity, TKey> :
    NMemoryKeyTableDataRepository<TEntity, TKey>,
    IWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected NMemoryWritableKeyTableDataRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
        ExpectIdentityKey = true;
    }

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>
    ///     Gets or sets a value indicating whether the expect identity key.
    /// </summary>
    /// <value>
    ///     True if expect identity key, false if not.
    /// </value>
    public virtual bool ExpectIdentityKey { get; set; }

    private readonly Type[] _supportedIdentityTypes = {typeof(int), typeof(long)};

    /// <summary>
    ///     Adds entity.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Add(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (ExpectIdentityKey)
        {
            if (_supportedIdentityTypes.Contains(typeof(TKey)))
                Table.Insert(entity);
            else
                throw new InvalidOperationException(
                    $"Type \"{typeof(TKey)}\" is not supported for InsertAuto. Use ExpectIdentityKey=false and set a key!");
        }
        else
        {
            if (entity.Id.Equals(GetDefault(typeof(TKey))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
            Table.Insert(entity);
        }

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
    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default)
    {
        return Task.FromResult(Add(entity));
    }

    /// <summary>
    ///     Adds a range of entities.
    /// </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var e in entities)
            Add(e);
    }

    /// <summary>
    ///     Adds a range asynchronous.
    /// </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An asynchronous result.
    /// </returns>
    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default)
    {
        AddRange(entities);
        return Task.CompletedTask;
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
        var originalEntity = Get(entity.Id);
        if (originalEntity == null) throw new UpdateException(entity);

        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            Table.Update((TEntity) stampedEntity);
        }

        Table.Update(entity);
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
    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default)
    {
        return Task.FromResult(Update(entity));
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
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>
    ///     True if it succeeds, false if it fails.
    /// </returns>
    public virtual bool Delete(TKey id)
    {
        return Delete(Get(id));
    }

    /// <summary>
    ///     Deletes the asynchronous described by ID.
    /// </summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The delete.
    /// </returns>
    public virtual Task<bool> DeleteAsync(TKey id, CancellationToken ctx = default)
    {
        return Task.FromResult(Delete(id));
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
        var c1 = Table.Count;
        Table.Delete(entity);
        var c2 = Table.Count;
        return c1 != c2;
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
        return Task.FromResult(Delete(entity));
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

    /// <summary>   Gets a default.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The default.</returns>
    private static object GetDefault(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    #endregion
}