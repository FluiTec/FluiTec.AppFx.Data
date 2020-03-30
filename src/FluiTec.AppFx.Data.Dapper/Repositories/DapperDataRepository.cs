using System;
using System.Collections.Generic;
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
    public abstract class DapperDataRepository<TEntity> : IDataRepository<TEntity>
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

        /// <summary>   Gets the count. </summary>
        /// <returns>   An int. </returns>
        public virtual int Count()
        {
            var command = $"SELECT COUNT(*) FROM {TableName}";
            return UnitOfWork.Connection.ExecuteScalar<int>(command, null, UnitOfWork.Transaction);
        }

        #endregion
    }
}