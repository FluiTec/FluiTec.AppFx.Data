using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
/// An ef writable key table data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public class EfWritableKeyTableDataRepository<TEntity, TKey> : EfWritableTableDataRepository<TEntity>, IWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected EfWritableKeyTableDataRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

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
    public TEntity Get(TKey id)
    {
        return Set.Find(id);
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
    public async Task<TEntity> GetAsync(TKey id, CancellationToken ctx = default)
    {
        return await Set.FindAsync(new object[] { id }, ctx);
    }

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>
    /// Deletes the given ID.
    /// </summary>
    ///
    /// <param name="id">   The Identifier to delete. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    public bool Delete(TKey id)
    {
        var entity = Get(id);
        return Delete(entity);
    }

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
    public async Task<bool> DeleteAsync(TKey id, CancellationToken ctx = default)
    {
        var entity = await GetAsync(id, ctx);
        return await DeleteAsync(entity, ctx);
    }

    #endregion
}