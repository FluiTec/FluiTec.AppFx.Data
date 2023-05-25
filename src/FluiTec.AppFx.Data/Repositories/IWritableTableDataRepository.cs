using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   Interface for writable data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IWritableTableDataRepository<TEntity> : ITableRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Adds entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    TEntity Add(TEntity entity);

    /// <summary>   Adds an asynchronous to 'cancellationToken'. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The add. </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>   Adds a range. </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>   Adds a range asynchronous to 'cancellationToken'. </summary>
    /// <param name="entities">             An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   A Task. </returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>   Updates the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    TEntity Update(TEntity entity);

    /// <summary>   Updates the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The update. </returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    bool Delete(params object[] keys);

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="keys">                 A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The delete. </returns>
    Task<bool> DeleteAsync(object[] keys, CancellationToken cancellationToken = default);

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    bool Delete(TEntity entity);

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The delete. </returns>
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}