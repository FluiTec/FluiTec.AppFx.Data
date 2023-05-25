using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory;

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
        Transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        _ownsConnection = true;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    public NMemoryUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork) : base(logger, parentUnitOfWork.TransactionOptions)
    {
        if (parentUnitOfWork is NMemoryUnitOfWork nmUnitOfWork)
        {
            Transaction = nmUnitOfWork.Transaction;
            _ownsConnection = false;
        }
        else
        {
            Transaction = new TransactionScope(TransactionScopeOption.Required, parentUnitOfWork.TransactionOptions);
            _ownsConnection = true;
        }
    }

    /// <summary>   Gets or sets the transaction. </summary>
    /// <value> The transaction. </value>
    public TransactionScope? Transaction { get; private set; }

    /// <summary>   Gets a value indicating whether we can commit. </summary>
    /// <value> True if we can commit, false if not. </value>
    public override bool CanCommit
    {
        get
        {
            if (ParentUnitOfWork != null)
                return ParentUnitOfWork.CanCommit;
            return Transaction != null;
        }
    }

    /// <summary>   Commits no cancel. </summary>
    /// <exception cref="T:System.InvalidOperationException">   Thrown when the requested operation
    ///                                                         is invalid. </exception>
    protected override void CommitNoCancel()
    {
        if (!CanCommit || !_ownsConnection) return;
        Transaction!.Complete();
        Transaction!.Dispose();
        Transaction = null;
    }

    /// <summary>   Commits by parent no cancel. </summary>
    protected override void CommitByParentNoCancel()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>   Rolls back a no cancel. </summary>
    /// <exception cref="T:System.InvalidOperationException">   Thrown when the requested operation
    ///                                                         is invalid. </exception>
    protected override void RollbackNoCancel()
    {
        if (!CanCommit || !_ownsConnection) return;
        Transaction!.Dispose();
        Transaction = null;
    }

    /// <summary>   Rolls back a by parent no cancel. </summary>
    protected override void RollbackByParentNoCancel()
    {
        throw new System.NotImplementedException();
    }
}