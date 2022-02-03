using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper writable key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class DapperWritableKeyTableDataRepository<TEntity, TKey> :
    DapperKeyTableDataRepository<TEntity, TKey>,
    IWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperWritableKeyTableDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
        ExpectIdentityKey = true;
    }

    #endregion

    #region Methods

    /// <summary>   Gets a key. </summary>
    /// <param name="id">   The identifier. </param>
    /// <returns>   The key. </returns>
    protected static TKey GetKey(long id)
    {
        if (typeof(TKey) == typeof(int))
            return (TKey) (object) Convert.ToInt32(id);
        if (typeof(TKey) == typeof(uint))
            return (TKey) (object) Convert.ToUInt32(id);

        if (typeof(TKey) == typeof(long))
            return (TKey) (object) Convert.ToInt64(id);
        if (typeof(TKey) == typeof(ulong))
            return (TKey) (object) Convert.ToUInt64(id);

        if (typeof(TKey) == typeof(short))
            return (TKey) (object) Convert.ToInt16(id);
        if (typeof(TKey) == typeof(ushort))
            return (TKey) (object) Convert.ToUInt16(id);

        throw new NotImplementedException(
            "This repository only supports int/uint long/ulong short/ushort as primary key.");
    }

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>   Gets or sets a value indicating whether the expect identity key.</summary>
    /// <value> True if expect identity key, false if not.</value>
    public bool ExpectIdentityKey { get; set; }

    private readonly Type[] _supportedIdentityTypes = {typeof(int), typeof(long)};

    /// <summary>   Adds entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Add(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (ExpectIdentityKey)
        {
            if (_supportedIdentityTypes.Contains(typeof(TKey)))
            {
                var lkey = UnitOfWork.Connection.InsertAuto(entity, UnitOfWork.Transaction);
                entity.Id = GetKey(lkey);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Type \"{typeof(TKey)}\" is not supported for InsertAuto. Use ExpectIdentityKey=false and set a key!");
            }
        }
        else
        {
            if (entity.Id.Equals(GetDefault(typeof(TKey))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
            UnitOfWork.Connection.Insert(entity, UnitOfWork.Transaction);
        }

        return entity;
    }

    /// <summary>   Adds entity.</summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity.</returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (ExpectIdentityKey)
        {
            if (_supportedIdentityTypes.Contains(typeof(TKey)))
            {
                var lkey = await UnitOfWork.Connection.InsertAutoAsync(entity, UnitOfWork.Transaction);
                entity.Id = GetKey(lkey);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Type \"{typeof(TKey)}\" is not supported for InsertAuto. Use ExpectIdentityKey=false and set a key!");
            }
        }
        else
        {
            if (entity.Id.Equals(GetDefault(typeof(TKey))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
            await UnitOfWork.Connection.InsertAsync(entity, UnitOfWork.Transaction);
        }

        return entity;
    }

    /// <summary>   Adds a range of entities. </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (ExpectIdentityKey)
        {
            if (_supportedIdentityTypes.Contains(typeof(TKey)))
                UnitOfWork.Connection.InsertAutoMultiple(keyEntities, UnitOfWork.Transaction);
            else
                throw new InvalidOperationException(
                    $"Type \"{typeof(TKey)}\" is not supported for InsertAuto. Use ExpectIdentityKey=false and set a key!");
        }
        else
        {
            if (keyEntities.Any(e => e.Id.Equals(GetDefault(typeof(TKey)))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
            UnitOfWork.Connection.InsertMultiple(keyEntities, UnitOfWork.Transaction);
        }
    }

    /// <summary>   Adds a range asynchronous.</summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <returns>   An asynchronous result.</returns>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var keyEntities = entities as TEntity[] ?? entities.ToArray();

        foreach (var entity in keyEntities)
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

        if (ExpectIdentityKey)
        {
            if (_supportedIdentityTypes.Contains(typeof(TKey)))
                await UnitOfWork.Connection.InsertAutoMultipleAsync(keyEntities, UnitOfWork.Transaction);
            else
                throw new InvalidOperationException(
                    $"Type \"{typeof(TKey)}\" is not supported for InsertAuto. Use ExpectIdentityKey=false and set a key!");
        }
        else
        {
            if (keyEntities.Any(e => e.Id.Equals(GetDefault(typeof(TKey)))))
                throw new InvalidOperationException("EntityKey must be set to a non default value.");
            await UnitOfWork.Connection.InsertMultipleAsync(keyEntities, UnitOfWork.Transaction);
        }
    }

    /// <summary>   Updates the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
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

    /// <summary>   Updates the asynchronous described by entity.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The update.</returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity is ITimeStampedKeyEntity stampedEntity)
        {
            var originalTimeStamp = stampedEntity.TimeStamp;
            stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow);

            if (await UnitOfWork.Connection.UpdateAsync(entity, originalTimeStamp, UnitOfWork.Transaction))
                return entity;
            throw new UpdateException(entity);
        }

        if (await UnitOfWork.Connection.UpdateAsync(entity, UnitOfWork.Transaction))
            return entity;
        throw new UpdateException(entity);
    }

    /// <summary>   Deletes the given ID. </summary>
    /// <param name="id">   The Identifier to delete. </param>
    public virtual bool Delete(TKey id)
    {
        return UnitOfWork.Connection.Delete<TEntity>(id, UnitOfWork.Transaction);
    }

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>   An asynchronous result.</returns>
    public virtual Task<bool> DeleteAsync(TKey id)
    {
        return UnitOfWork.Connection.DeleteAsync<TEntity>(id, UnitOfWork.Transaction);
    }

    /// <summary>   Deletes the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    public virtual bool Delete(TEntity entity)
    {
        return Delete(entity.Id);
    }

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The delete.</returns>
    public virtual Task<bool> DeleteAsync(TEntity entity)
    {
        return DeleteAsync(entity.Id);
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