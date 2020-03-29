using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories
{
    /// <summary>Interface for a writable, table based repository with keys.</summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <seealso cref="FluiTec.AppFx.Data.Repositories.IKeyTableDataRepository{TEntity, TKey}" />
    public interface IWritableKeyTableDataRepository<out TEntity, in TKey> : IKeyTableDataRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>, new()
    {

    }
}