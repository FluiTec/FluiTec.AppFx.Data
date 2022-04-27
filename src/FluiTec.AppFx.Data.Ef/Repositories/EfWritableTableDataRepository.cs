using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
/// An ef writable data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class EfWritableTableDataRepository<TEntity> : EfDataRepository<TEntity>, IWritableTableDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    public EfWritableTableDataRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

    #endregion

    #region IWritableTableDataRepository

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public TEntity Add(TEntity entity)
    {
        Set.Add(entity);
        return entity;
    }

    /// <summary>
    /// Adds entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default)
    {
        var result = await Set.AddAsync(entity, ctx);
        return result.Entity;
    }

    /// <summary>
    /// Adds a range of entities.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public void AddRange(IEnumerable<TEntity> entities)
    {
        Set.AddRange(entities);
    }

    /// <summary>
    /// Adds a range asynchronous.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// An asynchronous result.
    /// </returns>
    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default)
    {
        return Set.AddRangeAsync(entities, ctx);
    }

    /// <summary>
    /// Updates the given entity.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// A TEntity.
    /// </returns>
    public TEntity Update(TEntity entity)
    {
        Set.Attach(entity);
        UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

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
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ctx = default)
    {
        Set.Attach(entity);
        UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Deletes the given ID.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    ///
    /// <returns>
    /// True if it succeeds, false if it fails.
    /// </returns>
    public bool Delete(TEntity entity)
    {
        var entry = Set.Remove(entity);
        return entry.State == EntityState.Deleted;
    }

    /// <summary>
    /// Deletes the asynchronous described by ID.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    /// <param name="ctx">      (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The delete.
    /// </returns>
    public Task<bool> DeleteAsync(TEntity entity, CancellationToken ctx = default)
    {
        var entry = Set.Remove(entity);
        return Task.FromResult(entry.State == EntityState.Deleted);
    }

    #endregion
}