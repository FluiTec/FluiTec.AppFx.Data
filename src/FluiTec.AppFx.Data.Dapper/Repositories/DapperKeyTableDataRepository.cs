using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class DapperKeyTableDataRepository<TEntity, TKey> : DapperWritableTablaDataRepository<TEntity>,
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
        return UnitOfWork.Connection.Get<TEntity>(SqlBuilder, id, UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="id">   The Identifier to use. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    public virtual Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default)
    {
        return UnitOfWork.Connection.GetAsync<TEntity>(SqlBuilder, id, UnitOfWork.Transaction, cancellationToken: ctx);
    }
}