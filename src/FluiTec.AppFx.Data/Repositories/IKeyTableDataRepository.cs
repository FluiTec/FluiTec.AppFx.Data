using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>Interface for a table based repository with keys.</summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="FluiTec.AppFx.Data.Repositories.ITableDataRepository{TEntity}" />
public interface IKeyTableDataRepository<TEntity, in TKey> : ITableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>	Gets an entity using the given identifier. </summary>
    /// <param name="id">	The Identifier to use. </param>
    /// <returns>	A TEntity. </returns>
    TEntity Get(TKey id);

    /// <summary>
    /// Gets an entity asynchronous.
    /// </summary>
    ///
    /// <param name="id">   The Identifier to use. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default);
}

/// <summary>
/// Interface for key table data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey1">    Type of the key 1. </typeparam>
/// <typeparam name="TKey2">    Type of the key 2. </typeparam>
public interface IKeyTableDataRepository<TEntity, in TKey1, in TKey2> : ITableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>	Gets an entity using the given identifiers. </summary>
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    TEntity Get(TKey1 key1, TKey2 key2);

    /// <summary>
    /// Gets an entity using the given identifiers.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The asynchronous entity.
    /// </returns>
    Task<TEntity> GetAsync(TKey1 key1, TKey2 key2, CancellationToken ctx = default);
}

/// <summary>
/// Interface for key table data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey1">    Type of the key 1. </typeparam>
/// <typeparam name="TKey2">    Type of the key 2. </typeparam>
/// <typeparam name="TKey3">    Type of the key 3. </typeparam>
public interface IKeyTableDataRepository<TEntity, in TKey1, in TKey2, in TKey3> : ITableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Gets an entity using the given identifiers.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="key3"> The third key. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    TEntity Get(TKey1 key1, TKey2 key2, TKey3 key3);

    /// <summary>
    /// Gets an entity using the given identifiers.
    /// </summary>
    ///
    /// <param name="key1"> The first key. </param>
    /// <param name="key2"> The second key. </param>
    /// <param name="key3"> The third key. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The asynchronous.
    /// </returns>
    Task<TEntity> GetAsync(TKey1 key1, TKey2 key2, TKey3 key3, CancellationToken ctx = default);
}