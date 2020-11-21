using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories
{
    /// <summary>   A lite database key table data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <typeparam name="TKey">     Type of the key. </typeparam>
    public abstract class LiteDbKeyTableDataRepository<TEntity, TKey> : LiteDbDataRepository<TEntity>,
        IKeyTableDataRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected LiteDbKeyTableDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
        {
        }

        #endregion

        #region IKeyTableDataRepository

        /// <summary>   Gets an entity using the given identifier. </summary>
        /// <param name="id">   The Identifier to use. </param>
        /// <returns>   A TEntity. </returns>
        public virtual TEntity Get(TKey id)
        {
            return Collection.FindById(GetBsonKey(id));
        }

        /// <summary>   Gets an entity asynchronous.</summary>
        /// <param name="id">   The Identifier to use. </param>
        /// <returns>   A TEntity.</returns>
        public virtual Task<TEntity> GetAsync(TKey id)
        {
            return Task.FromResult(Get(id));
        }

        #endregion

        #region Methods

        /// <summary>	Gets bson key. </summary>
        /// <param name="key">	The key. </param>
        /// <returns>	The bson key. </returns>
        protected abstract BsonValue GetBsonKey(TKey key);

        #endregion
    }
}