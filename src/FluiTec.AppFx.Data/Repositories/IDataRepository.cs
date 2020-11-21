using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary> Interface for a repository specialized on entities.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDataRepository<TEntity> : IRepository
        where TEntity : class, IEntity, new()
    {
        /// <summary>	Gets all entities in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        IEnumerable<TEntity> GetAll();

        /// <summary>   Gets all asynchronous.</summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>   Counts the number of records. </summary>
        /// <returns>   An int defining the total number of records. </returns>
        int Count();

        /// <summary>   Count asynchronous.</summary>
        /// <returns>   The count.</returns>
        Task<int> CountAsync();
    }
}