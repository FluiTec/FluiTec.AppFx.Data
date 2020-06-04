using System.Collections.Generic;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary>Interface for a writable, table based repository with keys.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="FluiTec.AppFx.Data.Repositories.IKeyTableDataRepository{TEntity, TKey}" />
    public interface IWritableKeyTableDataRepository<TEntity, in TKey> : IKeyTableDataRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        /// <summary>	Adds entity. </summary>
        /// <param name="entity">	The entity to add. </param>
        /// <returns>	A TEntity. </returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        TEntity Add(TEntity entity);

        /// <summary>	Adds a range of entities. </summary>
        /// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
        // ReSharper disable once UnusedMember.Global
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>	Updates the given entity. </summary>
        /// <param name="entity">	The entity to add. </param>
        /// <returns>	A TEntity. </returns>
        // ReSharper disable once UnusedMember.Global
        TEntity Update(TEntity entity);

        /// <summary>	Deletes the given ID. </summary>
        /// <param name="id">	The Identifier to delete. </param>
        // ReSharper disable once UnusedMemberInSuper.Global
        void Delete(TKey id);

        /// <summary>	Deletes the given entity. </summary>
        /// <param name="entity">	The entity to add. </param>
        // ReSharper disable once UnusedMember.Global
        void Delete(TEntity entity);
    }
}