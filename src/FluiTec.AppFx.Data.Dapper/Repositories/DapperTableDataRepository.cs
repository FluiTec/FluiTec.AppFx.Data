using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using FluiTec.AppFx.Data.Sql.Attributes;
using FluiTec.AppFx.Data.Sql.Models;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>
///     A dapper table data repository.
/// </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class DapperTableDataRepository<TEntity> : DapperDataRepository<TEntity>,
    ITableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperTableDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
        KeyProperties = SqlCache.TypeKeyPropertiesCache(EntityType)
            .OrderBy(kp => kp.ExtendedData.Order)
            .ToList();
    }

    #endregion

    #region Properties

    /// <summary>
    ///     Gets the key properties.
    /// </summary>
    /// <value>
    ///     The key properties.
    /// </value>
    protected IList<PropertyInfoEx<SqlKeyAttribute>> KeyProperties { get; }

    #endregion

    #region Methods

    /// <summary>
    ///     Gets a key.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>
    ///     The key.
    /// </returns>
    protected IDictionary<string, object> GetKey(params object[] keys)
    {
        var key = new ExpandoObject() as IDictionary<string, object>;

        for (var i = 0; i < keys.Length; i++) key.Add(KeyProperties[i].PropertyInfo.Name, keys[i]);

        return key;
    }

    #endregion

    #region ITableDataRepository

    /// <summary>
    ///     Gets an entity using the given identifier.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual TEntity Get(params object[] keys)
    {
        Logger?.LogTrace("Get<{type}>({keys})", typeof(TEntity), keys);
        return UnitOfWork.Connection.Get<TEntity>(SqlBuilder, GetKey(keys), UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        Logger?.LogTrace("GetAsync<{type}>({keys})", typeof(TEntity), keys);
        return UnitOfWork.Connection.GetAsync<TEntity>(SqlBuilder, GetKey(keys), UnitOfWork.Transaction,
            cancellationToken: ctx);
    }

    #endregion
}