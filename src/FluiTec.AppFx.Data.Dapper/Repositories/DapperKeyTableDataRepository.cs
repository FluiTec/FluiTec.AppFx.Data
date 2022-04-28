using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class DapperKeyTableDataRepository<TEntity, TKey> : DapperDataRepository<TEntity>,
    IKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperKeyTableDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    /// <summary>   Gets an entity using the given identifier. </summary>
    /// <param name="id">   The Identifier to use. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Get(TKey id)
    {
        return UnitOfWork.Connection.Get<TEntity>(id, UnitOfWork.Transaction);
    }

    /// <summary>
    /// Gets an entity using the given identifier.
    /// </summary>
    ///
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public TEntity Get(params object[] keys)
    {
        return UnitOfWork.Connection.Get<TEntity>(GetKey(keys), UnitOfWork.Transaction);
    }

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
    public Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default)
    {
        return UnitOfWork.Connection.GetAsync<TEntity>(id, UnitOfWork.Transaction, cancellationToken: ctx);
    }

    public Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        return UnitOfWork.Connection.GetAsync<TEntity>(GetKey(keys), UnitOfWork.Transaction);
    }

    /// <summary>
    /// Gets a key.
    /// </summary>
    ///
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    ///
    /// <returns>
    /// The key.
    /// </returns>
    private IDictionary<string, object> GetKey(params object[] keys)
    {
        var typeKeys = SqlCache.TypeKeyPropertiesCache(EntityType)
            .OrderBy(p => p.ExtendedData.Order)
            .ToArray();
        var key = new ExpandoObject() as IDictionary<string, object>;

        for (var i = 0; i < keys.Length; i++)
        {
            key.Add(typeKeys[i].PropertyInfo.Name, keys[i]);
        }

        return key;
    }

    /// <summary>
    ///     Map by identifier.
    /// </summary>
    /// <typeparam name="TpEntity"> Type of the TP entity. </typeparam>
    /// <typeparam name="TpKey">    Type of the TP key. </typeparam>
    /// <param name="dictionary">   The dictionary. </param>
    /// <param name="entity">       The entity. </param>
    /// <returns>
    ///     A TPEntity.
    /// </returns>
    // ReSharper disable once UnusedMember.Global
    public static TpEntity MapById<TpEntity, TpKey>(IDictionary<TpKey, TpEntity> dictionary, TpEntity entity)
        where TpEntity : class, IKeyEntity<TpKey>, new()
    {
        if (dictionary.TryGetValue(entity.Id, out var entry)) return entry;

        entry = entity;
        dictionary.Add(entity.Id, entity);

        return entry;
    }
}