namespace FluiTec.AppFx.Data;

/// <summary>   A messages. </summary>
public static class Messages
{
    /// <summary>   (Immutable) the unit of work finished. </summary>
    public const string UnitOfWorkFinished =
        "Unable to commit/rollback UnitOfWork, since it's already finished";

    /// <summary>   (Immutable) the unit of work parent controlled. </summary>
    public const string UnitOfWorkParentControlled =
        "Unable to commit/rollback UnitOfWork, since it's solely controlled by a parent UnitOfWork";

    /// <summary>   (Immutable) zero-based index of the invalid page. </summary>
    public const string InvalidPageIndex = "PageIndex must be: >=0 (and if appropriate < pageCount)!";

    /// <summary>   (Immutable) size of the invalid page. </summary>
    public const string InvalidPageSize = "PageSize must be: >0!";
}