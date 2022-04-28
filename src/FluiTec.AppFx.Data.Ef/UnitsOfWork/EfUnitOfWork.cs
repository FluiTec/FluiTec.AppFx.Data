using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.UnitsOfWork;

/// <summary>
///     An ef unit of work.
/// </summary>
public class EfUnitOfWork : UnitOfWork
{
    #region Fields

    /// <summary>True to owns connection.</summary>
    private readonly bool _ownsContext = true;

    #endregion

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
        if (Context != null)
            Rollback();
    }

    #endregion

    #region Properties

    public IDbContextTransaction Transaction { get; private set; }

    /// <summary>
    ///     Gets or sets the context.
    /// </summary>
    /// <value>
    ///     The context.
    /// </value>
    public IDynamicDbContext Context { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="logger">       The logger. </param>
    public EfUnitOfWork(IEfDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
    {
        Context = dataService.GetContext();
        Transaction = Context.Database.BeginTransaction();
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
    public EfUnitOfWork(EfUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger)
        : base(dataService, logger)
    {
        if (parentUnitOfWork == null) throw new ArgumentNullException(nameof(parentUnitOfWork));
        _ownsContext = false;
        Context = parentUnitOfWork.Context;
        Transaction = parentUnitOfWork.Transaction;
    }

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
        if (Context == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be committed since it's already finished. (Missing DbContext)");
        if (!_ownsContext) return;

        // clear transaction
        //Context.SaveChanges();
        Transaction.Commit();

        Transaction.Dispose();
        Context.Dispose();
        Transaction = null;
        Context = null;
    }

    /// <summary>
    ///     Rolls back the UnitOfWork.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    public override void Rollback()
    {
        if (Context == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be rolled back since it's already finished. (Missing transaction)");
        if (!_ownsContext) return;

        Transaction.Rollback();

        // clear transaction
        Transaction.Dispose();
        Context.Dispose();
        Transaction = null;
        Context = null;
    }

    #endregion
}