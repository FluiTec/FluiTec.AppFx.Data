using System;
using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.UnitsOfWork;

/// <summary>   A memory unit of work. </summary>
public class NMemoryUnitOfWork : ParentAwareUnitOfWork
{
    /// <summary>   (Immutable) true to owns connection. </summary>
    private readonly bool _ownsConnection;

    /// <summary>   Constructor. </summary>
    /// <param name="logger">               The logger. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    public NMemoryUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions)
        : base(logger, transactionOptions)
    {
        TransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions);
        _ownsConnection = true;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    public NMemoryUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork) : base(logger,
        parentUnitOfWork)
    {
        if (parentUnitOfWork is NMemoryUnitOfWork nmUnitOfWork)
        {
            TransactionScope = nmUnitOfWork.TransactionScope;
            ControlledByParent = false;
            _ownsConnection = false;
        }
        else
        {
            TransactionScope =
                new TransactionScope(TransactionScopeOption.RequiresNew, parentUnitOfWork.TransactionOptions);
            ControlledByParent = true;
            _ownsConnection = true;
        }
    }

    /// <summary>   Gets or sets the transaction. </summary>
    /// <value> The transaction. </value>
    public TransactionScope? TransactionScope { get; private set; }

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
        TransactionScope!.Complete();
        TransactionScope!.Dispose();
        TransactionScope = null;
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
        TransactionScope!.Dispose();
        TransactionScope = null;
    }
}