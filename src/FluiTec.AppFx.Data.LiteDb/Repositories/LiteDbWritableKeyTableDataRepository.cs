using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories;

/// <summary>   A lite database writable key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class LiteDbWritableKeyTableDataRepository<TEntity, TKey> :
    LiteDbKeyTableDataRepository<TEntity, TKey>,
    IWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    #region Constructors

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected LiteDbWritableKeyTableDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
        ExpectIdentityKey = true;
    }

    #endregion

    #region Methods

    /// <summary>	Gets a key. </summary>
    /// <param name="key">	The key. </param>
    /// <returns>	The key. </returns>
    protected abstract TKey GetKey(BsonValue key);

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>   Gets or sets a value indicating whether the expect identity key.</summary>
    /// <value> True if expect identity key, false if not.</value>
    public bool ExpectIdentityKey { get; set; }

    /// <summary>   Adds entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Add(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        entity.Id = GetKey(Collection.Insert(entity));
        return entity;
    }

    /// <summary>   Adds entity.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity.</returns>
    public virtual Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        entity.Id = GetKey(Collection.Insert(entity));
        return Task.FromResult(entity);
    }

    /// <summary>   Adds a range of entities. </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        foreach (var entity in keyEntities)
            Collection.Insert(entity);
    }

    /// <summary>   Adds a range asynchronous.</summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <returns>   An asynchronous result.</returns>
    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        AddRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>   Updates the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Update(TEntity entity)
    {
        var success = false;
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var inCollection = Collection.FindById(GetBsonKey(entity.Id));
            if (((ITimeStampedKeyEntity) inCollection).TimeStamp == stampedEntity.TimeStamp)
            {
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);
                success = Collection.Update(GetBsonKey(entity.Id), entity);
            }
        }
        else
        {
            success = Collection.Update(GetBsonKey(entity.Id), entity);
        }

        return success ? entity : throw new UpdateException(entity);
    }

    /// <summary>   Updates the asynchronous described by entity.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The update.</returns>
    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(Update(entity));
    }

    /// <summary>   Deletes the given ID. </summary>
    /// <param name="id">   The Identifier to delete. </param>
    public virtual bool Delete(TKey id)
    {
        return Collection.Delete(GetBsonKey(id));
    }

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>   The delete.</returns>
    public virtual Task<bool> DeleteAsync(TKey id)
    {
        return Task.FromResult(Delete(id));
    }

    /// <summary>   Deletes the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    public virtual bool Delete(TEntity entity)
    {
        return Collection.Delete(GetBsonKey(entity.Id));
    }

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The delete.</returns>
    public virtual Task<bool> DeleteAsync(TEntity entity)
    {
        return Task.FromResult(Delete(entity));
    }

    #endregion
}