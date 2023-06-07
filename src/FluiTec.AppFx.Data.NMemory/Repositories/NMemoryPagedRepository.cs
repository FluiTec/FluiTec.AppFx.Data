using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.Repositories;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>   A memory paged repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class NMemoryPagedRepository<TEntity> : NMemoryRepository<TEntity>, IPagedRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    public NMemoryPagedRepository(IDataService dataService, INMemoryDataProvider dataProvider) : base(dataService,
        dataProvider)
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
    public IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize)
    {
        pageIndex = PageHelper.FixPageIndex(pageIndex);
        pageSize = PageHelper.FixPageSize(pageSize, DataProvider.PageSettings);
        return Table.Skip(pageIndex * pageSize).Take(pageSize);
    }

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
    public IPagedResult<TEntity> GetPagedResult(int pageIndex, int pageSize)
    {
        pageIndex = PageHelper.FixPageIndex(pageIndex);
        pageSize = PageHelper.FixPageSize(pageSize, DataProvider.PageSettings);
        var pageCount = Count() / pageSize + (Count() % pageSize > 0 ? 1 : 0);
        var records = Table.Skip(pageIndex * pageSize).Take(pageSize);
        var result = new PagedResult<TEntity>(pageIndex, (int)pageCount, pageSize, records);
        return result;
    }

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
    public Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetPaged(pageIndex, pageSize));
    }

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
    public Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetPagedResult(pageIndex, pageSize));
    }
}