using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>   A dapper key table data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <typeparam name="TKey">     Type of the key. </typeparam>
    public abstract class DapperKeyTableDataRepository<TEntity, TKey> : DapperDataRepository<TEntity>,
        IKeyTableDataRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperKeyTableDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
        {
        }

        /// <summary>   Gets an entity using the given identifier. </summary>
        /// <param name="id">   The Identifier to use. </param>
        /// <returns>   A TEntity. </returns>
        public virtual TEntity Get(TKey id)
        {
            return UnitOfWork.Connection.Get<TEntity>(id, UnitOfWork.Transaction);
        }

        /// <summary>   Gets an entity asynchronous.</summary>
        /// <param name="id">   The Identifier to use. </param>
        /// <returns>	A TEntity. </returns>
        public Task<TEntity> GetAsync(TKey id)
        {
            return UnitOfWork.Connection.GetAsync<TEntity>(id, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Map by identifier.
        /// </summary>
        ///
        /// <typeparam name="TPEntity"> Type of the TP entity. </typeparam>
        /// <typeparam name="TPKey">    Type of the TP key. </typeparam>
        /// <param name="dictionary">   The dictionary. </param>
        /// <param name="entity">       The entity. </param>
        ///
        /// <returns>
        /// A TPEntity.
        /// </returns>
        public static TPEntity MapById<TPEntity, TPKey>(IDictionary<TPKey, TPEntity> dictionary, TPEntity entity)
            where TPEntity : class, IKeyEntity<TPKey>, new()
        {
            if (dictionary.TryGetValue(entity.Id, out var entry)) return entry;

            entry = entity;
            dictionary.Add(entity.Id, entity);

            return entry;
        }
    }
}