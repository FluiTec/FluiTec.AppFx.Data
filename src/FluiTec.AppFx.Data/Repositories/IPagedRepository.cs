using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   Interface for paged repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IPagedRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Gets the paged in this collection. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the paged in this collection.
    /// </returns>
    IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize);

    /// <summary>   Gets paged asynchronous. </summary>
    /// <param name="pageIndex">            Zero-based index of the page. </param>
    /// <param name="pageSize">             Size of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The paged. </returns>
    Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);
}