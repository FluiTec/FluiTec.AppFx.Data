using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper writable key table data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class DapperWritableKeyTableDataRepository<TEntity, TKey> :
    DapperKeyTableDataRepository<TEntity, TKey>,
    IWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperWritableKeyTableDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>   Deletes the given ID. </summary>
    /// <param name="id">   The Identifier to delete. </param>
    public virtual bool Delete(TKey id)
    {
        return UnitOfWork.Connection.Delete<TEntity>(id, UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Deletes the asynchronous described by ID.
    /// </summary>
    /// <param name="id">   The Identifier to delete. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An asynchronous result.
    /// </returns>
    public virtual Task<bool> DeleteAsync(TKey id, CancellationToken ctx = default)
    {
        return UnitOfWork.Connection.DeleteAsync<TEntity>(id, UnitOfWork.Transaction, cancellationToken: ctx);
    }

    #endregion
}