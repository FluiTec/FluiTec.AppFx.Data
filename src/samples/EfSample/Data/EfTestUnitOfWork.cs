﻿using EfSample.Data.Repositories;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace EfSample.Data;

/// <summary>
///     An ef test unit of work.
/// </summary>
public class EfTestUnitOfWork : EfUnitOfWork, ITestUnitOfWork
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="logger">       The logger. </param>
    public EfTestUnitOfWork(IEfDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
    {
        RegisterRepositories();
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <param name="dataService">      The data service. </param>
    /// <param name="logger">           The logger. </param>
    public EfTestUnitOfWork(EfUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) :
        base(
            parentUnitOfWork, dataService, logger)
    {
        RegisterRepositories();
    }

    /// <summary>
    ///     Gets the dummy repository.
    /// </summary>
    /// <value>
    ///     The dummy repository.
    /// </value>
    public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();

    /// <summary>
    ///     Registers the repositories.
    /// </summary>
    private void RegisterRepositories()
    {
        RepositoryProviders.Add(typeof(IDummyRepository),
            (uow, log) => new EfDummyRepository((EfTestUnitOfWork) uow, log));
    }
}