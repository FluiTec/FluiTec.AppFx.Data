using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>   A dapper pre defined key repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <typeparam name="TKey">     Type of the key. </typeparam>
    public abstract class
        DapperPreDefinedKeyRepository<TEntity, TKey> : DapperWritableKeyTableDataRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperPreDefinedKeyRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
        {
        }

        #endregion

        #region Methods

        /// <summary>	Sets insert key. </summary>
        /// <param name="entity">	The entity to add. </param>
        protected abstract void SetInsertKey(TEntity entity);

        #endregion

        #region IWritableKeyTableDataRepository

        /// <summary>	Adds entity. </summary>
        /// <param name="entity">	The entity to add. </param>
        /// <returns>	A TEntity. </returns>
        public override TEntity Add(TEntity entity)
        {
            SetInsertKey(entity);

            var builder = UnitOfWork.Connection.GetBuilder();
            var sql = builder.InsertAutoMultiple(EntityType);

            UnitOfWork.Connection.Execute(sql, entity, UnitOfWork.Transaction);

            return entity;
        }

        #endregion
    }
}