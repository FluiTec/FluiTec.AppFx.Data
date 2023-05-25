using System;
using System.Transactions;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>   A base unit of work. </summary>
public abstract class BaseUnitOfWork : IUnitOfWork
{
    /// <summary>   Event queue for all listeners interested in BeforeCommit events. </summary>
    public event EventHandler<CancelUnitOfWorkEventArgs>? BeforeCommit;

    /// <summary>   Event queue for all listeners interested in Commited events. </summary>
    public event EventHandler<UnitOfWorkEventArgs>? Commited;

    /// <summary>   Event queue for all listeners interested in BeforeRollback events. </summary>
    public event EventHandler<CancelUnitOfWorkEventArgs>? BeforeRollback;

    /// <summary>   Event queue for all listeners interested in Rolledback events. </summary>
    public event EventHandler<UnitOfWorkEventArgs>? Rolledback;
    
    protected BaseUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions)
    {
        Logger = logger;
        TransactionOptions = transactionOptions;
    }

    /// <summary>   Gets options for controlling the transaction. </summary>
    /// <value> Options that control the transaction. </value>
    public TransactionOptions TransactionOptions { get; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IUnitOfWork>? Logger { get; }

    /// <summary>   Gets a value indicating whether we can commit. </summary>
    /// <value> True if we can commit, false if not. </value>
    public virtual bool CanCommit => !IsFinished;

    /// <summary>   Gets or sets a value indicating whether this object is finished. </summary>
    /// <value> True if this object is finished, false if not. </value>
    public bool IsFinished { get; protected set; }

    /// <summary>   Raises the cancel unit of work event. </summary>
    /// <param name="e">    Event information to send to registered event handlers. </param>
    protected void OnBeforeCommit(CancelUnitOfWorkEventArgs e)
    {
        BeforeCommit?.Invoke(this, e);
    }

    /// <summary>   Raises the unit of work event. </summary>
    /// <param name="e">    Event information to send to registered event handlers. </param>
    protected void OnCommit(UnitOfWorkEventArgs e)
    {
        Commited?.Invoke(this, e);
    }

    /// <summary>   Raises the cancel unit of work event. </summary>
    /// <param name="e">    Event information to send to registered event handlers. </param>
    protected void OnBeforeRollback(CancelUnitOfWorkEventArgs e)
    {
        BeforeRollback?.Invoke(this, e);
    }

    /// <summary>   Raises the unit of work event. </summary>
    /// <param name="e">    Event information to send to registered event handlers. </param>
    protected void OnRollback(UnitOfWorkEventArgs e)
    {
        Rolledback?.Invoke(this, e);
    }

    /// <summary>   Commits this object. </summary>
    public void Commit()
    {
        if (IsFinished) 
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        var args = new CancelUnitOfWorkEventArgs(this);
        OnBeforeCommit(args);

        if (args.Cancel) return;
        IsFinished = true;
        CommitNoCancel();
        OnCommit(args);
    }

    /// <summary>   Commits no cancel. </summary>
    protected abstract void CommitNoCancel();

    /// <summary>   Rollbacks this object. </summary>
    public void Rollback()
    {
        if (IsFinished)
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        var args = new CancelUnitOfWorkEventArgs(this);
        OnBeforeRollback(args);

        if (args.Cancel) return;
        IsFinished = true;
        RollbackNoCancel();
        OnRollback(args);
    }

    /// <summary>   Rolls back a no cancel. </summary>
    protected abstract void RollbackNoCancel();
}