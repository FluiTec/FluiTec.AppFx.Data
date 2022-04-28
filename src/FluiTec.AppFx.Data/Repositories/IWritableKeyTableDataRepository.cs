using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>Interface for a writable, table based repository with keys.</summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="FluiTec.AppFx.Data.Repositories.IKeyTableDataRepository{TEntity, TKey}" />
public interface IWritableKeyTableDataRepository<TEntity, in TKey> : IKeyTableDataRepository<TEntity, TKey>, IWritableTableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>   Deletes the given ID.</summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>   True if it succeeds, false if it fails.</returns>
    bool Delete(TKey id);

    /// <summary>
    /// Deletes the asynchronous described by ID.
    /// </summary>
    ///
    /// <param name="id">   The Identifier to delete. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    Task<bool> DeleteAsync(TKey id, CancellationToken ctx = default);
}