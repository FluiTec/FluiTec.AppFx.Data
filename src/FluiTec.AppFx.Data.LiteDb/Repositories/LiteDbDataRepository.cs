using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories
{
    /// <summary>   A lite database data repository. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    public abstract class LiteDbDataRepository<TEntity> : IDataRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or
        ///     illegal values.
        /// </exception>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected LiteDbDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Logger = logger; // we accept null here
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

        #region Methods

        /// <summary>	Gets table name. </summary>
        /// <returns>	The table name. </returns>
        protected string GetTableName(Type t)
        {
            return UnitOfWork.LiteDbDataService.NameService.Name(t);
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
        public ILiteCollection<TEntity> Collection { get; }

        /// <summary>   Gets the logger. </summary>
        /// <value> The logger. </value>
        public ILogger<IRepository> Logger { get; }

        #endregion

        #region IDataRepository

        /// <summary>   Gets all entities in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Collection.FindAll();
        }

        /// <summary>   Gets all asynchronous.</summary>
        /// <returns>An enumerator that allows foreach to be used to process all items in this collection.</returns>
        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(GetAll());
        }

        /// <summary>   Counts the number of records. </summary>
        /// <returns>   An int defining the total number of records. </returns>
        public virtual int Count()
        {
            return Collection.Count();
        }

        /// <summary>   Count asynchronous.</summary>
        /// <returns>   The count.</returns>
        public Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        #endregion
    }
}