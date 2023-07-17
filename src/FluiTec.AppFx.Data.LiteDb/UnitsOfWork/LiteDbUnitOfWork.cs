using System;
using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.UnitsOfWork;

/// <summary> A lite database unit of work.</summary>
public class LiteDbUnitOfWork : ParentAwareUnitOfWork
{
    /// <summary>   (Immutable) true to owns connection. </summary>
    private readonly bool _ownsConnection;

    /// <summary> Constructor.</summary>
    /// <param name="logger">             The logger. </param>
    /// <param name="transactionOptions"> Options for controlling the transaction. </param>
    /// <param name="database">           The database. </param>
    public LiteDbUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions, LiteDatabase database) : base(logger, transactionOptions)
    {
        Database = database;
        _ownsConnection = true;
    }

    /// <summary> Constructor.</summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="database">         The database. </param>
    public LiteDbUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork, LiteDatabase database) : base(logger, parentUnitOfWork)
    {
        if (parentUnitOfWork is LiteDbUnitOfWork ldbUnitOfWork)
        {
            ControlledByParent = false;
            Database = ldbUnitOfWork.Database;
            _ownsConnection = false;
        }
        else
        {
            Database = database;
            ControlledByParent = true;
            _ownsConnection = true;
        }
    }

    /// <summary> Gets the database.</summary>
    /// <value> The database.</value>
    public LiteDatabase? Database { get; private set; }

    /// <summary>   Commits no cancel. </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///     Thrown when the requested operation
    ///     is invalid.
    /// </exception>
    protected override void CommitNoCancel()
    {
        base.CommitNoCancel();

        if (!CanCommit || !_ownsConnection)
            return;

        CommitInternal();
    }

    /// <summary>   Commits by parent no cancel. </summary>
    protected override void CommitByParentNoCancel()
    {
        if (IsFinished || !_ownsConnection)
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        CommitInternal();
    }

    /// <summary>   Commits an internal. </summary>
    protected virtual void CommitInternal()
    {
        Database!.Commit();
        Database = null;
    }

    /// <summary>   Rolls back a no cancel. </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///     Thrown when the requested operation
    ///     is invalid.
    /// </exception>
    protected override void RollbackNoCancel()
    {
        base.RollbackNoCancel();

        if (!CanCommit || !_ownsConnection)
            return;

        RollbackInternal();
    }

    /// <summary>   Rolls back a by parent no cancel. </summary>
    protected override void RollbackByParentNoCancel()
    {
        if (IsFinished || !_ownsConnection)
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        RollbackInternal();
    }

    /// <summary>   Rolls back an internal. </summary>
    protected virtual void RollbackInternal()
    {
        Database!.Dispose();
        Database = null;
    }
}