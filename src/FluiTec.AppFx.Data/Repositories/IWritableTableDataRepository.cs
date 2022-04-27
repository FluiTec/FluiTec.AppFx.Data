using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>
/// Interface for writable data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IWritableTableDataRepository<TEntity> : ITableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>	Adds entity. </summary>
    /// <param name="entity">	The entity to add. </param>
    /// <returns>	A TEntity. </returns>
    // ReSharper disable once UnusedMemberInSuper.Global
    TEntity Add(TEntity entity);

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default);

    /// <summary>	Adds a range of entities. </summary>
    /// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    // ReSharper disable once UnusedMember.Global
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds a range asynchronous.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// An asynchronous result.
    /// </returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default);

    /// <summary>	Updates the given entity. </summary>
    /// <param name="entity">	The entity to add. </param>
    /// <returns>	A TEntity. </returns>
    // ReSharper disable once UnusedMember.Global
    TEntity Update(TEntity entity);

    /// <summary>
    /// Updates the asynchronous described by entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The update.
    /// </returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default);

    /// <summary>   Deletes the given ID.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   True if it succeeds, false if it fails.</returns>
    bool Delete(TEntity entity);

    /// <summary>
    /// Deletes the asynchronous described by ID.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    Task<bool> DeleteAsync(TEntity entity, CancellationToken ctx = default);
}