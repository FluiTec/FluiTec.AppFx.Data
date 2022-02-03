using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>Interface for a writable, table based repository with keys.</summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="FluiTec.AppFx.Data.Repositories.IKeyTableDataRepository{TEntity, TKey}" />
public interface IWritableKeyTableDataRepository<TEntity, in TKey> : IKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    /// <summary>   Gets or sets a value indicating whether the expect identity key.</summary>
    /// <value> True if expect identity key, false if not.</value>
    // ReSharper disable once UnusedMemberInSuper.Global
    bool ExpectIdentityKey { get; set; }

    /// <summary>	Adds entity. </summary>
    /// <param name="entity">	The entity to add. </param>
    /// <returns>	A TEntity. </returns>
    // ReSharper disable once UnusedMemberInSuper.Global
    TEntity Add(TEntity entity);

    /// <summary>   Adds entity.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>	Adds a range of entities. </summary>
    /// <param name="entities">	An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    // ReSharper disable once UnusedMember.Global
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>   Adds a range asynchronous.</summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <returns>   An asynchronous result.</returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>	Updates the given entity. </summary>
    /// <param name="entity">	The entity to add. </param>
    /// <returns>	A TEntity. </returns>
    // ReSharper disable once UnusedMember.Global
    TEntity Update(TEntity entity);

    /// <summary>   Updates the asynchronous described by entity.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The update.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>   Deletes the given ID.</summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>   True if it succeeds, false if it fails.</returns>
    bool Delete(TKey id);

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <returns>   The delete.</returns>
    Task<bool> DeleteAsync(TKey id);

    /// <summary>   Deletes the given ID.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   True if it succeeds, false if it fails.</returns>
    bool Delete(TEntity entity);

    /// <summary>   Deletes the asynchronous described by ID.</summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The delete.</returns>
    Task<bool> DeleteAsync(TEntity entity);
}