using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Paging;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   A paged repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class PagedRepository<TEntity> : Repository<TEntity>, IPagedRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    protected PagedRepository(IDataService dataService, IDataProvider dataProvider) : base(dataService, dataProvider)
    {
    }

    /// <summary>   Gets the paged in this collection. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the paged in this collection.
    /// </returns>
    public IEnumerable<TEntity> GetPaged(int pageIndex)
    {
        return GetPaged(pageIndex, DataProvider.PageSettings.PageSize);
    }

    /// <summary>   Gets the paged in this collection. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process the paged in this collection.
    /// </returns>
    public abstract IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize);

    /// <summary>   Gets paged result. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <returns>   The paged result. </returns>
    public IPagedResult<TEntity> GetPagedResult(int pageIndex)
    {
        return GetPagedResult(pageIndex, DataProvider.PageSettings.PageSize);
    }

    /// <summary>   Gets paged result. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <returns>   The paged result. </returns>
    public abstract IPagedResult<TEntity> GetPagedResult(int pageIndex, int pageSize);

    /// <summary>   Gets paged asynchronous. </summary>
    /// <param name="pageIndex">            Zero-based index of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The paged. </returns>
    public Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, CancellationToken cancellationToken = default)
    {
        return GetPagedAsync(pageIndex, DataProvider.PageSettings.PageSize, cancellationToken);
    }

    /// <summary>   Gets paged asynchronous. </summary>
    /// <param name="pageIndex">            Zero-based index of the page. </param>
    /// <param name="pageSize">             Size of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The paged. </returns>
    public abstract Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);

    /// <summary>   Gets paged result asynchronous. </summary>
    /// <param name="pageIndex">            Zero-based index of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The paged result. </returns>
    public Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, CancellationToken cancellationToken = default)
    {
        return GetPagedResultAsync(pageIndex, DataProvider.PageSettings.PageSize, cancellationToken);
    }

    /// <summary>   Gets paged result asynchronous. </summary>
    /// <param name="pageIndex">            Zero-based index of the page. </param>
    /// <param name="pageSize">             Size of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The paged result. </returns>
    public abstract Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default);
}