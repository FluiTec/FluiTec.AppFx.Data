namespace FluiTec.AppFx.Data.Paging;

/// <summary>   A page helper. </summary>
public static class PageHelper
{
    /// <summary>   Fix page index. </summary>
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <returns>   An int. </returns>
    public static int FixPageIndex(int pageIndex)
    {
        return pageIndex < 0 ? 0 : pageIndex;
    }

    /// <summary>   Fix page size. </summary>
    /// <param name="pageSize"> Size of the page. </param>
    /// <param name="settings"> Options for controlling the operation. </param>
    /// <returns>   An int. </returns>
    public static int FixPageSize(int pageSize, PageSettings settings)
    {
        var fixedSize = pageSize < 1 ? 1 : pageSize;
        return fixedSize > settings.MaxPageSize ? settings.MaxPageSize : fixedSize;
    }
}