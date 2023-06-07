using System;
using System.Transactions;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>   A parent aware unit of work. </summary>
public abstract class ParentAwareUnitOfWork : BaseUnitOfWork
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="logger">               The logger. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    protected ParentAwareUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions)
        : base(logger, transactionOptions)
    {
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    protected ParentAwareUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork)
        : base(logger, parentUnitOfWork.TransactionOptions)
    {
        ControlledByParent = true;
        ParentUnitOfWork = parentUnitOfWork;
        ParentUnitOfWork.BeforeCommit += ParentUnitOfWork_BeforeCommit;
        ParentUnitOfWork.BeforeRollback += ParentUnitOfWork_BeforeRollback;
    }

    /// <summary>   Gets a value indicating whether we can commit. </summary>
    /// <value> True if we can commit, false if not. </value>
    public override bool CanCommit => !ControlledByParent && !IsFinished;

    /// <summary>   True to controlled by parent. </summary>
    public bool ControlledByParent { get; protected set; }

    /// <summary>   Gets the parent unit of work. </summary>
    /// <value> The parent unit of work. </value>
    public IUnitOfWork? ParentUnitOfWork { get; }

    /// <summary>   Event handler. Called by ParentUnitOfWork for before commit events. </summary>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Cancel unit of work event information. </param>
    private void ParentUnitOfWork_BeforeCommit(object sender, CancelUnitOfWorkEventArgs e)
    {
        if (!ControlledByParent)
            return;

        var args = new CancelUnitOfWorkEventArgs(this);
        OnBeforeCommit(args);

        if (args.Cancel)
        {
            e.Cancel = true;
            return;
        }

        CommitByParent(args);
    }

    /// <summary>   Event handler. Called by ParentUnitOfWork for before rollback events. </summary>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Cancel unit of work event information. </param>
    private void ParentUnitOfWork_BeforeRollback(object sender, CancelUnitOfWorkEventArgs e)
    {
        if (!ControlledByParent)
            return;

        var args = new CancelUnitOfWorkEventArgs(this);
        OnBeforeRollback(args);

        if (args.Cancel)
        {
            e.Cancel = true;
            return;
        }

        RollbackByParent(args);
    }

    /// <summary>   Commits no cancel. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    protected override void CommitNoCancel()
    {
        if (ControlledByParent)
            throw new InvalidOperationException(Messages.UnitOfWorkParentControlled);
    }

    /// <summary>   Commits by parent. </summary>
    protected void CommitByParent(UnitOfWorkEventArgs args)
    {
        CommitByParentNoCancel();
        IsFinished = true;
        OnCommit(args);
    }

    /// <summary>   Commits by parent no cancel. </summary>
    protected abstract void CommitByParentNoCancel();

    /// <summary>   Rolls back a no cancel. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    protected override void RollbackNoCancel()
    {
        if (ControlledByParent)
            throw new InvalidOperationException(Messages.UnitOfWorkParentControlled);
    }

    /// <summary>   Rolls back a by parent. </summary>
    protected void RollbackByParent(UnitOfWorkEventArgs args)
    {
        RollbackByParentNoCancel();
        IsFinished = true;
        OnRollback(args);
    }

    /// <summary>   Rolls back a by parent no cancel. </summary>
    protected abstract void RollbackByParentNoCancel();

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting
    ///     unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    ///     True to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && ControlledByParent && !IsFinished)
            ParentUnitOfWork!.Rollback();
        else
            base.Dispose(disposing);
    }
}