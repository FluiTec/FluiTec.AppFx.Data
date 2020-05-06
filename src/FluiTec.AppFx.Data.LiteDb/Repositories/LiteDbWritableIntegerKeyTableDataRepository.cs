using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories
{
    /// <summary>   A lite database writable integer key table data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    public class
        LiteDbWritableIntegerKeyTableDataRepository<TEntity> : LiteDbWritableKeyTableDataRepository<TEntity, int>
        where TEntity : class, IKeyEntity<int>, new()
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbWritableIntegerKeyTableDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) :
            base(unitOfWork, logger)
        {
        }

        #endregion

        #region LiteDbWritableKeyTableDataRepository

        /// <summary>   Gets bson key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The bson key. </returns>
        protected override BsonValue GetBsonKey(int key)
        {
            return key;
        }

        /// <summary>   Gets a key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The key. </returns>
        protected override int GetKey(BsonValue key)
        {
            return key;
        }

        #endregion
    }
}