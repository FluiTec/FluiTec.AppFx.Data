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
    /// <summary>   A lite database data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    public abstract class LiteDbDataRepository<TEntity>  : IDataRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region Constructors

        protected LiteDbDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger)
        {
            UnitOfWork = unitOfWork as LiteDbUnitOfWork;
            if (UnitOfWork == null)
                throw new ArgumentException(
                    $"{nameof(unitOfWork)} was either null or does not implement {nameof(LiteDbUnitOfWork)}!");

            var replacedDot = GetTableName(typeof(TEntity)).Replace('.', '_');
            var split = replacedDot.Split('_');
            if (split.Length > 1)
            {
                split[0] = new string(split[0].Where(char.IsUpper).ToArray());
                replacedDot = string.Concat(split);
            }

            TableName = replacedDot;

            // ReSharper disable once VirtualMemberCallInConstructor
            Collection = UnitOfWork.LiteDbDataService.Database.GetCollection<TEntity>(TableName);
        }

        #endregion

        #region Properties

        /// <summary>   Gets the name of the table. </summary>
        /// <value> The name of the table. </value>
        public virtual string TableName { get; }

        /// <summary>   Gets the unit of work. </summary>
        /// <value> The unit of work. </value>
        public LiteDbUnitOfWork UnitOfWork { get; }

        /// <summary>	Gets the collection. </summary>
        /// <value>	The collection. </value>
        public LiteCollection<TEntity> Collection { get; }

        #endregion

        #region Methods

        /// <summary>	Gets table name. </summary>
        /// <returns>	The table name. </returns>
        protected string GetTableName(Type t)
        {
            return UnitOfWork.LiteDbDataService.NameService.Name(t);
        }

        #endregion

        #region IDataRepository

        /// <summary>   Gets all entities in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public IEnumerable<TEntity> GetAll()
        {
            return Collection.FindAll();
        }

        /// <summary>   Counts the number of records. </summary>
        /// <returns>   An int defining the total number of records. </returns>
        public int Count()
        {
            return Collection.Count();
        }

        #endregion
    }
}
