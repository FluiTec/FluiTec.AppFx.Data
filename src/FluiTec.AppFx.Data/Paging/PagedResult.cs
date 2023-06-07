using System;
using System.Collections.Generic;
using System.Linq;

namespace FluiTec.AppFx.Data.Paging;

/// <summary>   Encapsulates the result of a paged. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class PagedResult<TEntity> : IPagedResult<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageCount">    Number of pages. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <param name="records">      The records. </param>
    public PagedResult(int pageIndex, int pageCount, int pageSize, IEnumerable<TEntity>? records)
    {
        PageIndex = pageIndex;
        PageCount = pageCount;
        PageSize = pageSize;
        Records = records ?? Enumerable.Empty<TEntity>();

        if (pageIndex < 0)
            throw new InvalidOperationException(Messages.InvalidPageIndex);
        if (pageIndex >= pageCount)
            throw new InvalidOperationException(Messages.InvalidPageIndex);
        if (pageSize <= 0)
            throw new InvalidOperationException(Messages.InvalidPageSize);
    }

    /// <summary>   Gets the zero-based index of the page. </summary>
    /// <value> The page index. </value>
    public int PageIndex { get; }

    /// <summary>   Gets the number of pages. </summary>
    /// <value> The number of pages. </value>
    public int PageCount { get; }

    /// <summary>   Gets the page size. </summary>
    /// <value> The size of the page. </value>
    public int PageSize { get; }

    /// <summary>   Gets the records. </summary>
    /// <value> The records. </value>
    public IEnumerable<TEntity> Records { get; }

    /// <summary>   Gets a value indicating whether this object has results. </summary>
    /// <value> True if this object has results, false if not. </value>
    public bool HasResults => Records.Any();

    /// <summary>   Gets a value indicating whether this object has next page. </summary>
    /// <value> True if this object has next page, false if not. </value>
    public bool HasNextPage => PageIndex + 1 < PageCount;

    /// <summary>   Gets a value indicating whether this object has previous page. </summary>
    /// <value> True if this object has previous page, false if not. </value>
    public bool HasPreviousPage => PageIndex > 0;
}