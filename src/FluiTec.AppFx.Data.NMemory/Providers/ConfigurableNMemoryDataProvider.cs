using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using NMemory;

namespace FluiTec.AppFx.Data.NMemory.Providers;

/// <summary>   A configurable n memory data provider. </summary>
public abstract class
    ConfigurableNMemoryDataProvider<TDataService, TUnitOfWork> : NMemoryDataProvider<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="configureDelegate">    A function delegate that yields a Database. </param>
    /// <param name="dataService">          The data service. </param>
    protected ConfigurableNMemoryDataProvider(Func<Database> configureDelegate, TDataService dataService) :
        base(dataService)
    {
        ConfigureDelegate = configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate));
    }

    /// <summary>   Gets the configure delegate. </summary>
    /// <value> A function delegate that yields a Database. </value>
    public Func<Database> ConfigureDelegate { get; }

    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected override Database ConfigureDatabase()
    {
        return ConfigureDelegate();
    }
}