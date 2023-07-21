using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>   A memory writable repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class NMemoryWritableTableRepository<TEntity> : NMemoryTableRepository<TEntity>,
    IWritableTableDataRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    public NMemoryWritableTableRepository(IDataService dataService, INMemoryDataProvider dataProvider,
        IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
    }

    /// <summary>   Adds entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public TEntity Add(TEntity entity)
    {
        Table.Insert(entity);
        return entity;
    }

    /// <summary>   Adds an asynchronous to 'cancellationToken'. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The add. </returns>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Add(entity));
    }

    /// <summary>   Adds a range. </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    public void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            Table.Insert(entity);
    }

    /// <summary>   Adds a range asynchronous to 'cancellationToken'. </summary>
    /// <param name="entities">             An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   A Task. </returns>
    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        AddRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>   Updates the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public TEntity Update(TEntity entity)
    {
        Table.Update(entity);
        return entity;
    }

    /// <summary>   Updates the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The update. </returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Update(entity));
    }

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool Delete(params object[] keys)
    {
        var org = Get(keys);
        return org != null && Delete(org);
    }

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="keys">                 A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The delete. </returns>
    public Task<bool> DeleteAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Delete(keys));
    }

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public bool Delete(TEntity entity)
    {
        var preDelete = Table.Count;
        Table.Delete(entity);
        var postDelete = Table.Count;
        return preDelete < postDelete;
    }

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The delete. </returns>
    public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Delete(entity));
    }
}