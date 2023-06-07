using System.Collections.Generic;

namespace FluiTec.AppFx.Data.Paging;

/// <summary>   Interface for paged result. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IPagedResult<out TEntity>
    where TEntity : class, new()
{
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
    public bool HasResults { get; }

    /// <summary>   Gets a value indicating whether this object has next page. </summary>
    /// <value> True if this object has next page, false if not. </value>
    public bool HasNextPage { get; }

    /// <summary>   Gets a value indicating whether this object has previous page. </summary>
    /// <value> True if this object has previous page, false if not. </value>
    public bool HasPreviousPage { get; }
}