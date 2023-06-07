namespace FluiTec.AppFx.Data.Paging;

/// <summary>   A page settings. </summary>
public class PageSettings
{
    /// <summary>   (Immutable) the default page size. </summary>
    public const int DefaultPageSize = 20;

    /// <summary>   (Immutable) the default maximum page size. </summary>
    public const int DefaultMaxPageSize = 100;

    /// <summary>   Constructor. </summary>
    /// <param name="pageSize">     Size of the page. </param>
    /// <param name="maxPageSize">  The maximum size of the page. </param>
    public PageSettings(int pageSize = DefaultPageSize, int maxPageSize = DefaultMaxPageSize)
    {
        PageSize = pageSize;
        MaxPageSize = maxPageSize;
    }

    /// <summary>   Gets the page size. </summary>
    /// <value> The size of the page. </value>
    public int PageSize { get; }

    /// <summary>   Gets the maximum size of the page. </summary>
    /// <value> The maximum size of the page. </value>
    public int MaxPageSize { get; }
}