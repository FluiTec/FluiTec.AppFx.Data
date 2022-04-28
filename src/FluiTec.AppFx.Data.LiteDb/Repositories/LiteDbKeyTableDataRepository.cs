using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories;

/// <summary>   A lite database key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class LiteDbKeyTableDataRepository<TEntity, TKey> : LiteDbDataRepository<TEntity>,
    IKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    #region Constructors

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected LiteDbKeyTableDataRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    #endregion

    #region Methods

    /// <summary>	Gets bson key. </summary>
    /// <param name="key">	The key. </param>
    /// <returns>	The bson key. </returns>
    protected abstract BsonValue GetBsonKey(TKey key);

    #endregion

    #region IKeyTableDataRepository

    /// <summary>
    /// Gets an entity using the given identifier.
    /// </summary>
    ///
    /// <param name="id">   The Identifier to use. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public virtual TEntity Get(TKey id)
    {
        return Collection.FindById(GetBsonKey(id));
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
        var typeKeys = SqlCache.TypeKeyPropertiesCache(typeof(TEntity))
            .OrderBy(tk => tk.ExtendedData.Order)
            .ToList();

        var sbPredicate = new StringBuilder();
        var keyDict = new Dictionary<string, BsonValue>();
        for (var i = 0; i < typeKeys.Count; i++)
        {
            if (i > 0)
                sbPredicate.Append(", ");
            sbPredicate.Append($"$.{typeKeys[i].PropertyInfo.Name} = {typeKeys[i].PropertyInfo.Name}");
            keyDict.Add(typeKeys[i].PropertyInfo.Name, new BsonValue(keys[i]));
        }

        return Collection.FindOne(sbPredicate.ToString(), new BsonDocument(keyDict));
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
    public virtual Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default)
    {
        return Task.FromResult(Get(id));
    }

    /// <summary>
    /// Gets an entity asynchronous.
    /// </summary>
    ///
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        return Task.FromResult(Get(keys));
    }

    #endregion
}