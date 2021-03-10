using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories
{
    /// <summary>   A dapper data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    public abstract class DapperDataRepository<TEntity> : IDataRepository<TEntity>, IRepositoryCommandCache
        where TEntity : class, IEntity, new()
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Logger = logger; // we accept null here

            SqlBuilder = unitOfWork.Connection.GetBuilder();
            EntityType = typeof(TEntity);
            TableName = GetTableName();
        }

        #endregion

        #region IRepositoryCommandCache

        /// <summary>   Gets from cache.</summary>
        /// <param name="commandFunc">  The command function. </param>
        /// <param name="memberName">   Name of the member. </param>
        /// <returns>   The data that was read from the cache.</returns>
        public string GetFromCache(Func<string> commandFunc, [CallerMemberName] string memberName = null)
        {
            return UnitOfWork.DapperDataService.GetFromCache(GetType(), memberName, commandFunc);
        }

        #endregion

        #region Properties

        /// <summary>   Gets the unit of work. </summary>
        /// <value> The unit of work. </value>
        public DapperUnitOfWork UnitOfWork { get; }

        /// <summary>   Gets the logger. </summary>
        /// <value> The logger. </value>
        public ILogger<IRepository> Logger { get; }

        /// <summary>	Gets the type of the entity. </summary>
        /// <value>	The type of the entity. </value>
        public Type EntityType { get; }

        /// <summary>   Gets the name of the table. </summary>
        /// <value> The name of the table. </value>
        public virtual string TableName { get; }

        /// <summary>	Gets the SQL builder. </summary>
        /// <value>	The SQL builder. </value>
        protected SqlBuilder SqlBuilder { get; }

        #endregion

        #region Methods

        /// <summary>   Gets table name. </summary>
        /// <returns>   The table name. </returns>
        protected string GetTableName()
        {
            return GetTableName(EntityType);
        }

        /// <summary>	Gets table name. </summary>
        /// <returns>	The table name. </returns>
        protected string GetTableName(Type t)
        {
            return SqlBuilder.Adapter.RenderTableName(t);
        }

        #endregion

        #region IDataRepository

        /// <summary>   Gets all entities in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.Connection.GetAll<TEntity>(UnitOfWork.Transaction);
        }

        /// <summary>   Gets all asynchronous.</summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return UnitOfWork.Connection.GetAllAsync<TEntity>(UnitOfWork.Transaction);
        }

        /// <summary>   Gets the count. </summary>
        /// <returns>   An int. </returns>
        public virtual int Count()
        {
            var command = GetFromCache(() => $"SELECT COUNT(*) FROM {TableName}");
            return UnitOfWork.Connection.ExecuteScalar<int>(command, null, UnitOfWork.Transaction);
        }

        /// <summary>   Count asynchronous.</summary>
        /// <returns>   The count.</returns>
        public virtual Task<int> CountAsync()
        {
            var command = GetFromCache(() => $"SELECT COUNT(*) FROM {TableName}");
            return UnitOfWork.Connection.ExecuteScalarAsync<int>(command, null, UnitOfWork.Transaction);
        }

        #endregion
    }
}