using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary>Interface for a table based repository with keys.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="FluiTec.AppFx.Data.Repositories.ITableDataRepository{TEntity}" />
    public interface IKeyTableDataRepository<TEntity, in TKey> : ITableDataRepository<TEntity>
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        /// <summary>	Gets an entity using the given identifier. </summary>
        /// <param name="id">	The Identifier to use. </param>
        /// <returns>	A TEntity. </returns>
        TEntity Get(TKey id);
    }
}