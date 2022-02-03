using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.UnitsOfWork;

/// <summary>
///     A memory unit of work.
/// </summary>
public class NMemoryUnitOfWork : UnitOfWork
{
    #region Fields

    /// <summary>True to owns connection.</summary>
    private readonly bool _ownsConnection = true;

    #endregion

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="logger">       The logger. </param>
    public NMemoryUnitOfWork(INMemoryDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
    {
        NMemoryDataService = dataService;
        TransactionScope = new TransactionScope();
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="dataService">      The data service. </param>
    /// <param name="logger">           The logger. </param>
    public NMemoryUnitOfWork(NMemoryUnitOfWork parentUnitOfWork, IDataService dataService,
        ILogger<IUnitOfWork> logger)
        : base(dataService, logger)
    {
        NMemoryDataService = dataService as INMemoryDataService;

        if (parentUnitOfWork == null) throw new ArgumentNullException(nameof(parentUnitOfWork));
        _ownsConnection = false;
        TransactionScope = parentUnitOfWork.TransactionScope;
    }

    #region IDisposable

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting
    ///     unmanaged resources.
    /// </summary>
    /// <param name="disposing">
    ///     true to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (TransactionScope != null)
            Rollback();
    }

    #endregion

    #region Properties

    /// <summary>   Gets or sets the transaction. </summary>
    /// <value> The transaction. </value>
    public TransactionScope TransactionScope { get; private set; }

    /// <summary>
    ///     Gets the memory data service.
    /// </summary>
    /// <value>
    ///     The n memory data service.
    /// </value>
    public INMemoryDataService NMemoryDataService { get; }

    #endregion

    #region IUnitOfWork

    /// <summary>   Gets or sets the logger factory. </summary>
    /// <summary>   Commits the UnitOfWork. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    public override void Commit()
    {
        if (TransactionScope == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be committed since it's already finished. (Missing transaction)");

        if (!_ownsConnection) return;

        // clear transaction
        TransactionScope.Complete();
        TransactionScope.Dispose();
        TransactionScope = null;
    }

    /// <summary>   Rolls back the UnitOfWork. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    public override void Rollback()
    {
        if (TransactionScope == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be rolled back since it's already finished. (Missing transaction)");

        if (!_ownsConnection) return;

        // clear transaction
        TransactionScope.Dispose();
        TransactionScope = null;
    }

    #endregion
}