using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork.Fixtures;

/// <summary>   A test parent aware unit of work. </summary>
public class TestParentAwareUnitOfWork : ParentAwareUnitOfWork
{
    /// <summary>
    ///     Gets or sets a value indicating whether the committed by parent no cancel.
    /// </summary>
    /// <value> True if committed by parent no cancel, false if not. </value>
    public bool CommittedByParentNoCancel { get; private set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the rolled back by parent no cancel.
    /// </summary>
    /// <value> True if rolled back by parent no cancel, false if not. </value>
    public bool RolledBackByParentNoCancel { get; private set; }

    /// <summary>   Constructor. </summary>
    /// <param name="logger">               The logger. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    public TestParentAwareUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions) : base(logger, transactionOptions)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    public TestParentAwareUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork) : base(logger, parentUnitOfWork)
    {
    }

    /// <summary>   Commits by parent no cancel. </summary>
    protected override void CommitByParentNoCancel()
    {
        CommittedByParentNoCancel = true;
    }

    /// <summary>   Rolls back a by parent no cancel. </summary>
    protected override void RollbackByParentNoCancel()
    {
        RolledBackByParentNoCancel = true;
    }
}