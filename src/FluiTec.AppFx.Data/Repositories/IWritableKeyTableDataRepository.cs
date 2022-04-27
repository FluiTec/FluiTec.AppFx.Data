using System.Collections.Generic;
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
    /// <summary>   Gets or sets a value indicating whether the expect identity key.</summary>
    /// <value> True if expect identity key, false if not.</value>
    // ReSharper disable once UnusedMemberInSuper.Global
    bool ExpectIdentityKey { get; set; }

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

/// <summary>
/// Interface for writable key table data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey1">    Type of the key 1. </typeparam>
/// <typeparam name="TKey2">    Type of the key 2. </typeparam>
public interface IWritableKeyTableDataRepository<TEntity, in TKey1, in TKey2> : IKeyTableDataRepository<TEntity, TKey1, TKey2>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Gets or sets a value indicating whether the expect identity key.
    /// </summary>
    ///
    /// <value>
    /// True if expect identity key, false if not.
    /// </value>
    bool ExpectIdentityKey { get; set; }

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Adds an asynchronous.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The add.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default);

    /// <summary>
    /// Adds a range.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds a range asynchronous.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A Task.
    /// </returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default);

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
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

    /// <summary>
    /// Deletes this object.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    bool Delete(TKey1 key1, TKey2 key2);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    Task<bool> DeleteAsync(TKey1 key1, TKey2 key2, CancellationToken ctx = default);

    /// <summary>
    /// Deletes this object.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    bool Delete(TEntity entity);

    /// <summary>
    /// Deletes the asynchronous.
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

/// <summary>
/// Interface for writable key table data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey1">    Type of the key 1. </typeparam>
/// <typeparam name="TKey2">    Type of the key 2. </typeparam>
/// <typeparam name="TKey3">    Type of the key 3. </typeparam>
public interface IWritableKeyTableDataRepository<TEntity, in TKey1, in TKey2, in TKey3> : IKeyTableDataRepository<TEntity, TKey1, TKey2, TKey3>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Gets or sets a value indicating whether the expect identity key.
    /// </summary>
    ///
    /// <value>
    /// True if expect identity key, false if not.
    /// </value>
    bool ExpectIdentityKey { get; set; }

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Adds an asynchronous.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The add.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default);

    /// <summary>
    /// Adds a range.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Adds a range asynchronous.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A Task.
    /// </returns>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default);

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
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

    /// <summary>
    /// Deletes this object.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="key3"> The third key. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    bool Delete(TKey1 key1, TKey2 key2, TKey3 key3);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="key3"> The third key. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    Task<bool> DeleteAsync(TKey1 key1, TKey2 key2, TKey3 key3, CancellationToken ctx = default);

    /// <summary>
    /// Deletes this object.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    bool Delete(TEntity entity);

    /// <summary>
    /// Deletes the asynchronous.
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