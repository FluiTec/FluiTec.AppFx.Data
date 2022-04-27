using System;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.UnitsOfWork;

/// <summary>
/// An ef unit of work.
/// </summary>
public class EfUnitOfWork : UnitOfWork
{
    #region Properties

    /// <summary>
    /// Gets or sets the context.
    /// </summary>
    ///
    /// <value>
    /// The context.
    /// </value>
    public DynamicDbContext Context { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="dataService">  The data service. </param>
    /// <param name="logger">       The logger. </param>
    public EfUnitOfWork(IEfDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
    {
        Context = dataService.GetContext();
    }

    #endregion

    #region IUnitOfWork

    /// <summary>   Gets or sets the logger factory. </summary>
    /// <summary>   Commits the UnitOfWork. </summary>
    ///
    /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                 invalid. </exception>
    public override void Commit()
    {
        if (Context == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be committed since it's already finished. (Missing DbContext)");

        // clear transaction
        Context.SaveChanges();
        Context.Dispose();
        Context = null;
    }

    /// <summary>
    /// Rolls back the UnitOfWork.
    /// </summary>
    ///
    /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                 invalid. </exception>
    public override void Rollback()
    {
        if (Context == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be rolled back since it's already finished. (Missing transaction)");

        // clear transaction
        Context.Dispose();
        Context = null;
    }

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
}