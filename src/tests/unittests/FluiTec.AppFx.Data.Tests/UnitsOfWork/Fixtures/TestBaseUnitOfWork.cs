using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork.Fixtures;

/// <summary>   A test base unit of work. </summary>
public class TestBaseUnitOfWork : BaseUnitOfWork
{
    /// <summary>   Constructor. </summary>
    /// <param name="logger">               The logger. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    public TestBaseUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions) : base(logger,
        transactionOptions)
    {
    }

    /// <summary>   Gets or sets a value indicating whether the committed no cancel. </summary>
    /// <value> True if committed no cancel, false if not. </value>
    public bool CommittedNoCancel { get; private set; }

    /// <summary>   Gets or sets a value indicating whether the rolled back no cancel. </summary>
    /// <value> True if rolled back no cancel, false if not. </value>
    public bool RolledBackNoCancel { get; private set; }

    /// <summary>   Commits no cancel. </summary>
    protected override void CommitNoCancel()
    {
        CommittedNoCancel = true;
    }

    /// <summary>   Rolls back a no cancel. </summary>
    protected override void RollbackNoCancel()
    {
        RolledBackNoCancel = true;
    }
}