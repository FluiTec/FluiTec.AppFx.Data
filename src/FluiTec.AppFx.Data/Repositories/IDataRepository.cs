using System.Collections.Generic;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary> Interface for a repository specialized on entities.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDataRepository<out TEntity> : IRepository
        where TEntity : class, IEntity, new()
    {
        /// <summary>	Gets all entities in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>Gets the count.</summary>
        /// <returns>An int.</returns>
        int Count();
    }
}