using System;
using System.Data;
using System.Transactions;
using FluiTec.AppFx.Data.Dapper.Providers;
using FluiTec.AppFx.Data.Sql.StatementProviders;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using IsolationLevel = System.Data.IsolationLevel;

namespace FluiTec.AppFx.Data.Dapper.UnitsOfWork;

/// <summary>   A dapper unit of work. </summary>
public class DapperUnitOfWork : ParentAwareUnitOfWork
{
    /// <summary>   (Immutable) true to owns connection. </summary>
    private readonly bool _ownsConnection;

    /// <summary>   Constructor. </summary>
    /// <param name="logger">               The logger. </param>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <param name="dataProvider">         The data provider. </param>
    public DapperUnitOfWork(ILogger<IUnitOfWork>? logger, TransactionOptions transactionOptions,
        IDapperDataProvider dataProvider)
        : base(logger, transactionOptions)
    {
        DataProvider = dataProvider;
        StatementProvider = DataProvider.StatementProvider;

        Connection = dataProvider.ConnectionFactory.CreateConnection(dataProvider.ConnectionString);
        Connection.Open();
        Transaction = Connection.BeginTransaction(Convert(transactionOptions.IsolationLevel));
        _ownsConnection = true;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="dataProvider">     The data provider. </param>
    public DapperUnitOfWork(ILogger<IUnitOfWork>? logger, IUnitOfWork parentUnitOfWork,
        IDapperDataProvider dataProvider) : base(logger,
        parentUnitOfWork)
    {
        DataProvider = dataProvider;
        StatementProvider = DataProvider.StatementProvider;

        if (parentUnitOfWork is DapperUnitOfWork dapperUnitOfWork)
        {
            Connection = dapperUnitOfWork.Connection;
            Transaction = dapperUnitOfWork.Transaction;
            ControlledByParent = false;
            _ownsConnection = false;
        }
        else
        {
            Connection = dataProvider.ConnectionFactory.CreateConnection(dataProvider.ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction(Convert(parentUnitOfWork.TransactionOptions.IsolationLevel));
            ControlledByParent = true;
            _ownsConnection = true;
        }
    }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public IDapperDataProvider DataProvider { get; }

    /// <summary> Gets the statement provider.</summary>
    /// <value> The statement provider.</value>
    public IStatementProvider StatementProvider { get; }

    /// <summary>   Gets or sets the connection. </summary>
    /// <value> The connection. </value>
    public IDbConnection Connection { get; protected set; }

    /// <summary>   Gets or sets the transaction. </summary>
    /// <value> The transaction. </value>
    public IDbTransaction Transaction { get; protected set; }

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
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    protected override void CommitByParentNoCancel()
    {
        if (IsFinished || !_ownsConnection)
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        CommitInternal();
    }

    /// <summary>   Commits an internal. </summary>
    protected virtual void CommitInternal()
    {
        // clear transaction
        Transaction.Commit();
        Transaction.Dispose();
        Transaction = null!;

        // clear connection
        Connection.Close();
        Connection.Dispose();
        Connection = null!;
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
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    protected override void RollbackByParentNoCancel()
    {
        if (IsFinished || !_ownsConnection)
            throw new InvalidOperationException(Messages.UnitOfWorkFinished);

        RollbackInternal();
    }

    /// <summary>   Rolls back an internal. </summary>
    protected virtual void RollbackInternal()
    {
        // clear transaction
        Transaction.Rollback();
        Transaction.Dispose();
        Transaction = null!;

        // clear connection
        Connection.Close();
        Connection.Dispose();
        Connection = null!;
    }

    /// <summary>   Converts the given transaction level. </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside
    ///     the required range.
    /// </exception>
    /// <param name="transactionLevel"> The transaction level. </param>
    /// <returns>   An IsolationLevel. </returns>
    protected IsolationLevel Convert(System.Transactions.IsolationLevel transactionLevel)
    {
        return transactionLevel switch
        {
            System.Transactions.IsolationLevel.Chaos => IsolationLevel.Chaos,
            System.Transactions.IsolationLevel.ReadCommitted => IsolationLevel.ReadCommitted,
            System.Transactions.IsolationLevel.ReadUncommitted => IsolationLevel.ReadUncommitted,
            System.Transactions.IsolationLevel.RepeatableRead => IsolationLevel.RepeatableRead,
            System.Transactions.IsolationLevel.Serializable => IsolationLevel.Serializable,
            System.Transactions.IsolationLevel.Snapshot => IsolationLevel.Snapshot,
            System.Transactions.IsolationLevel.Unspecified => IsolationLevel.Unspecified,
            _ => throw new ArgumentOutOfRangeException(nameof(transactionLevel), transactionLevel, null)
        };
    }
}