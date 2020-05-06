using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories
{
    /// <summary>   A lite database integer key table data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    public abstract class LiteDbIntegerKeyTableDataRepository<TEntity> : LiteDbKeyTableDataRepository<TEntity, int>
        where TEntity : class, IKeyEntity<int>, new()
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected LiteDbIntegerKeyTableDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
        {
        }

        /// <summary>   Gets bson key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The bson key. </returns>
        protected override BsonValue GetBsonKey(int key)
        {
            return key;
        }
    }
}