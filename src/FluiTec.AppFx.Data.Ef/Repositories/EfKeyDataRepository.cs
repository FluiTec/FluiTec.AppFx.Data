using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
/// An ef key data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
/// <typeparam name="TKey">     Type of the key. </typeparam>
public class EfKeyDataRepository<TEntity, TKey> : EfDataRepository<TEntity>, IKeyTableDataRepository<TEntity, TKey>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected EfKeyDataRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

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
        return Set.Find(keys);
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
        return await Set.FindAsync(new object[] {id}, ctx);
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
    public async Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default)
    {
        return await Set.FindAsync(keys, ctx);
    }
}