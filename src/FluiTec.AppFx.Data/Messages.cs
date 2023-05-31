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
}