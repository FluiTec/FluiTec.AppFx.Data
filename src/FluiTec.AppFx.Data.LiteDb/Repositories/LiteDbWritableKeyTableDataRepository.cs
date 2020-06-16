using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories
{
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
        }

        #endregion

        #region Methods

        /// <summary>	Gets a key. </summary>
        /// <param name="key">	The key. </param>
        /// <returns>	The key. </returns>
        protected abstract TKey GetKey(BsonValue key);

        #endregion

        #region IWritableKeyTableDataRepository

        /// <summary>   Adds entity. </summary>
        /// <param name="entity">   The entity to add. </param>
        /// <returns>   A TEntity. </returns>
        public virtual TEntity Add(TEntity entity)
        {
            if (entity is ITimeStampedKeyEntity stampedEntity)
                stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            entity.Id = GetKey(Collection.Insert(entity));
            return entity;
        }

        /// <summary>   Adds a range of entities. </summary>
        /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            var keyEntities = entities as TEntity[] ?? entities.ToArray();

            foreach (var entity in keyEntities)
                if (entity is ITimeStampedKeyEntity stampedEntity)
                    stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            foreach (var entity in keyEntities)
                Collection.Insert(entity);
        }

        /// <summary>   Updates the given entity. </summary>
        /// <param name="entity">   The entity to add. </param>
        /// <returns>   A TEntity. </returns>
        public virtual TEntity Update(TEntity entity)
        {
            if (entity is ITimeStampedKeyEntity stampedEntity)
            {
                var inCollection = Collection.FindById(GetBsonKey(entity.Id));
                if (((ITimeStampedKeyEntity) inCollection).TimeStamp == stampedEntity.TimeStamp)
                {
                    stampedEntity.TimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                    Collection.Update(GetBsonKey(entity.Id), entity);
                }
            }
            else
            {
                Collection.Update(GetBsonKey(entity.Id), entity);
            }

            return entity;
        }

        /// <summary>   Deletes the given ID. </summary>
        /// <param name="id">   The Identifier to delete. </param>
        public virtual void Delete(TKey id)
        {
            Collection.Delete(GetBsonKey(id));
        }

        /// <summary>   Deletes the given entity. </summary>
        /// <param name="entity">   The entity to add. </param>
        public virtual void Delete(TEntity entity)
        {
            Collection.Delete(GetBsonKey(entity.Id));
        }

        #endregion
    }
}