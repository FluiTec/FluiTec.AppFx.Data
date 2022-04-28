using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>
///     A memory key table data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public class NMemoryKeyTableDataRepository<TEntity, TKey> : NMemoryDataRepository<TEntity>,
    IKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected NMemoryKeyTableDataRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    /// <summary>   Gets an entity using the given identifier. </summary>
    /// <param name="id">   The Identifier to use. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Get(TKey id)
    {
        return Table.SingleOrDefault(e => e.Id.Equals(id));
    }

    /// <summary>
    ///     Gets an entity using the given identifier.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public TEntity Get(params object[] keys)
    {
        return Table.SingleOrDefault(entity => KeysMatch(entity, keys));
    }

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="id">   The Identifier to use. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default)
    {
        return Task.FromResult(Get(id));
    }

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        return Task.FromResult(Table.SingleOrDefault(entity => KeysMatch(entity, keys)));
    }

    /// <summary>
    ///     Keys match.
    /// </summary>
    /// <param name="entity">   The entity. </param>
    /// <param name="keys">     The keys. </param>
    /// <returns>
    ///     True if it succeeds, false if it fails.
    /// </returns>
    private bool KeysMatch(TEntity entity, IReadOnlyList<object> keys)
    {
        var typeKeys = SqlCache.TypeKeyPropertiesCache(EntityType)
            .OrderBy(tk => tk.ExtendedData.Order)
            .ToList();
        return !typeKeys.Where((t, i) => !t.PropertyInfo.GetValue(entity, null).Equals(keys[i])).Any();
    }
}