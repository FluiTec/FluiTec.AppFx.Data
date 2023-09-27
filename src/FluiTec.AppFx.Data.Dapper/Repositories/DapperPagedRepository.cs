using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary> A dapper paged repository.</summary>
/// <typeparam name="TEntity"> Type of the entity. </typeparam>
public class DapperPagedRepository<TEntity> : DapperRepository<TEntity>, IPagedRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary> Constructor.</summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public DapperPagedRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork)
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
        var skipRecords = pageIndex * pageSize;
        var takeRecords = pageIndex * pageSize;
        var sql = UnitOfWork.StatementProvider.GetPagingStatement(TypeSchema, nameof(skipRecords), nameof(takeRecords));

        return UnitOfWork.Connection.Query<TEntity>(sql, new[] { skipRecords, takeRecords }, UnitOfWork.Transaction,
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
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
        var count = Count();
        var pageCount = count / pageSize + (count % pageSize > 0 ? 1 : 0);
        var records = GetPaged(pageIndex, pageSize);
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
        pageIndex = PageHelper.FixPageIndex(pageIndex);
        pageSize = PageHelper.FixPageSize(pageSize, DataProvider.PageSettings);
        var skipRecords = pageIndex * pageSize;
        var takeRecords = pageIndex * pageSize;
        var sql = UnitOfWork.StatementProvider.GetPagingStatement(TypeSchema, nameof(skipRecords), nameof(takeRecords));

        var query = new CommandDefinition(sql, new[] { skipRecords, takeRecords }, UnitOfWork.Transaction,
            (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds,
            cancellationToken: cancellationToken);
        return UnitOfWork.Connection.QueryAsync<TEntity>(query);
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
    public async Task<IPagedResult<TEntity>> GetPagedResultAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await CountAsync(cancellationToken);
        var pageCount = count / pageSize + (count % pageSize > 0 ? 1 : 0);
        var records = await GetPagedAsync(pageIndex, pageSize, cancellationToken);
        var result = new PagedResult<TEntity>(pageIndex, (int)pageCount, pageSize, records);
        return result;
    }
}