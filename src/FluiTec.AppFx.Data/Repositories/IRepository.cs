using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   Interface for repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Gets all items in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>   Gets all asynchronous. </summary>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   all. </returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>   Gets the count. </summary>
    /// <returns>   A long. </returns>
    long Count();

    /// <summary>   Count asynchronous. </summary>
    /// <returns>   The count. </returns>
    Task<long> CountAsync();
}