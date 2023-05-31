using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>   Interface for unit of work. </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>   Gets options for controlling the transaction. </summary>
    /// <value> Options that control the transaction. </value>
    TransactionOptions TransactionOptions { get; }

    public ILogger<IUnitOfWork>? Logger { get; }

    /// <summary>   Gets a value indicating whether we can commit. </summary>
    /// <value> True if we can commit, false if not. </value>
    bool CanCommit { get; }

    /// <summary>   Gets a value indicating whether this object is finished. </summary>
    /// <value> True if this object is finished, false if not. </value>
    bool IsFinished { get; }

    /// <summary>   Event queue for all listeners interested in BeforeCommit events. </summary>
    event EventHandler<CancelUnitOfWorkEventArgs>? BeforeCommit;

    /// <summary>   Event queue for all listeners interested in Commited events. </summary>
    event EventHandler<UnitOfWorkEventArgs>? Commited;

    /// <summary>   Event queue for all listeners interested in BeforeRollback events. </summary>
    event EventHandler<CancelUnitOfWorkEventArgs>? BeforeRollback;

    /// <summary>   Event queue for all listeners interested in Rolledback events. </summary>
    event EventHandler<UnitOfWorkEventArgs>? Rolledback;

    /// <summary>   Commits this object. </summary>
    void Commit();

    /// <summary>   Rollbacks this object. </summary>
    void Rollback();
}

/// <summary>   Interface for unit of work. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
public interface IUnitOfWork<out TDataService> : IUnitOfWork
    where TDataService : IDataService
{
    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    TDataService DataService { get; }
}