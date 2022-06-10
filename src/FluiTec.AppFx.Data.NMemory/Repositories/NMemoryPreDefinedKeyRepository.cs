using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>
/// A memory pre defined key repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public abstract class NMemoryPreDefinedKeyRepository<TEntity, TKey> : NMemoryWritableKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IKeyEntity<TKey>, new()
{
    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected NMemoryPreDefinedKeyRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

    #region Methods

    /// <summary>	Sets insert key. </summary>
    /// <param name="entity">	The entity to add. </param>
    protected abstract void SetInsertKey(TEntity entity);

    #endregion

    #region IWritableKeyTableDataRepository

    /// <summary>	Adds entity. </summary>
    /// <param name="entity">	The entity to add. </param>
    /// <returns>	A TEntity. </returns>
    public override TEntity Add(TEntity entity)
    {
        SetInsertKey(entity);
        return base.Add(entity);
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
    public override Task<TEntity> AddAsync(TEntity entity, CancellationToken ctx = default)
    {
        SetInsertKey(entity);
        return base.AddAsync(entity, ctx);
    }

    /// <summary>
    /// Adds a range of entities.
    /// </summary>
    ///
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
    public override void AddRange(IEnumerable<TEntity> entities)
    {
        var entries = entities.ToList();
        foreach (var e in entries)
            SetInsertKey(e);

        base.AddRange(entries);
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
    public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ctx = default)
    {
        var entries = entities.ToList();
        foreach (var e in entries)
            SetInsertKey(e);

        return base.AddRangeAsync(entries, ctx);
    }

    #endregion
}