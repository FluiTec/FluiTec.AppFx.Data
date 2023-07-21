using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.LiteDb.Repositories;

/// <summary> A lite database paged repository.</summary>
/// <typeparam name="TEntity"> Type of the entity. </typeparam>
public class LiteDbPagedRepository<TEntity> : LiteDbRepository<TEntity>, IPagedRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public LiteDbPagedRepository(IDataService dataService, ILiteDbDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
    }

    /// <summary> Gets the paged in this collection.</summary>
    /// <param name="pageIndex"> Zero-based index of the page. </param>
    /// <returns>An enumerator that allows foreach to be used to process the paged in this collection.</returns>
    public IEnumerable<TEntity> GetPaged(int pageIndex)
    {
        return GetPaged(pageIndex, DataProvider.PageSettings.PageSize);
    }

    /// <summary> Gets the paged in this collection.</summary>
    /// <param name="pageIndex"> Zero-based index of the page. </param>
    /// <param name="pageSize">  Size of the page. </param>
    /// <returns>An enumerator that allows foreach to be used to process the paged in this collection.</returns>
    public IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize)
    {
        pageIndex = PageHelper.FixPageIndex(pageIndex);
        pageSize = PageHelper.FixPageSize(pageSize, DataProvider.PageSettings);
        return Collection.Query().Limit(pageSize).Offset(pageIndex * pageSize).ToEnumerable();
    }

    /// <summary> Gets paged result.</summary>
    /// <param name="pageIndex"> Zero-based index of the page. </param>
    /// <returns> The paged result.</returns>
    public IPagedResult<TEntity> GetPagedResult(int pageIndex)
    {
        return GetPagedResult(pageIndex, DataProvider.PageSettings.PageSize);
    }

    /// <summary> Gets paged result.</summary>
    /// <param name="pageIndex"> Zero-based index of the page. </param>
    /// <param name="pageSize">  Size of the page. </param>
    /// <returns> The paged result.</returns>
    public IPagedResult<TEntity> GetPagedResult(int pageIndex, int pageSize)
    {
        pageIndex = PageHelper.FixPageIndex(pageIndex);
        pageSize = PageHelper.FixPageSize(pageSize, DataProvider.PageSettings);
        var pageCount = Count() / pageSize + (Count() % pageSize > 0 ? 1 : 0);
        var records = Collection.Query().Limit(pageSize).Offset(pageIndex * pageSize).ToEnumerable();
        var result = new PagedResult<TEntity>(pageIndex, (int)pageCount, pageSize, records);
        return result;
    }

    /// <summary> Gets paged asynchronous.</summary>
    /// <param name="pageIndex">         Zero-based index of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns> The paged.</returns>
    public Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, CancellationToken cancellationToken = default)
    {
        return GetPagedAsync(pageIndex, DataProvider.PageSettings.PageSize, cancellationToken);
    }

    /// <summary> Gets paged asynchronous.</summary>
    /// <param name="pageIndex">         Zero-based index of the page. </param>
    /// <param name="pageSize">          Size of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns> The paged.</returns>
    public Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetPaged(pageIndex, pageSize));
    }

    /// <summary> Gets paged result asynchronous.</summary>
    /// <param name="pageIndex">         Zero-based index of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns> The paged result.</returns>
    public Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, CancellationToken cancellationToken = default)
    {
        return GetPagedResultAsync(pageIndex, DataProvider.PageSettings.PageSize, cancellationToken);
    }

    /// <summary> Gets paged result asynchronous.</summary>
    /// <param name="pageIndex">         Zero-based index of the page. </param>
    /// <param name="pageSize">          Size of the page. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns> The paged result.</returns>
    public Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetPagedResult(pageIndex, pageSize));
    }
}