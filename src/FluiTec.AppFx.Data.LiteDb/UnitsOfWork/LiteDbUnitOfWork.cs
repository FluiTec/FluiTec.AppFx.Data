﻿using System;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.UnitsOfWork;

/// <summary>   A lite database unit of work. </summary>
public class LiteDbUnitOfWork : UnitOfWork
{
    #region Fields

    /// <summary>True to owns connection.</summary>
    private readonly bool _ownsConnection = true;

    #endregion

    #region IDisposable

    /// <summary>
    ///     Releases the unmanaged resources used by the FluiTec.AppFx.Data.Dapper.DapperDataService and
    ///     optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">
    ///     True to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        if (TransactionCheck != null)
            Rollback();
    }

    #endregion

    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null. </exception>
    /// <param name="dataService">  The data service. </param>
    /// <param name="logger">       The logger. </param>
    public LiteDbUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger)
        : base(dataService, logger)
    {
        LiteDbDataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        dataService.Database.BeginTrans();
        TransactionCheck = new object();
    }

    /// <summary>   Constructor. </summary>
    /// <param name="dataService">      The data service. </param>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="logger">           The logger. </param>
    public LiteDbUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork,
        ILogger<IUnitOfWork> logger)
        : base(dataService, logger)
    {
        _ownsConnection = false;
        LiteDbDataService = dataService;
        TransactionCheck = parentUnitOfWork.TransactionCheck;
    }

    #endregion

    #region Properties

    /// <summary>   Gets or sets the data service. </summary>
    /// <value> The data service. </value>
    public ILiteDbDataService LiteDbDataService { get; }

    /// <summary>
    ///     Gets or sets the transaction check.
    /// </summary>
    /// <value>
    ///     The transaction check.
    /// </value>
    internal object TransactionCheck { get; set; }

    #endregion Properties

    #region IUnitOfWork

    /// <summary>	Commits this object. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    public override void Commit()
    {
        if (TransactionCheck == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be committed since it's already finished. (Missing transaction)");

        if (!_ownsConnection) return;
        LiteDbDataService.Database.Commit();
        TransactionCheck = null;
    }

    /// <summary>	Rollbacks this object. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    public override void Rollback()
    {
        if (TransactionCheck == null)
            throw new InvalidOperationException(
                "UnitOfWork can't be rolled back since it's already finished. (Missing transaction)");

        if (!_ownsConnection) return;
        LiteDbDataService.Database.Rollback();
        TransactionCheck = null;
    }

    #endregion
}