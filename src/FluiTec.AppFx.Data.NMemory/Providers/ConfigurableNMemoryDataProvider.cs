using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Options;
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
    /// <param name="options">              Options for controlling the operation. </param>
    protected ConfigurableNMemoryDataProvider(Func<Database> configureDelegate, TDataService dataService,
        DataOptions options) :
        base(dataService, options)
    {
        ConfigureDelegate = configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate));
    }

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="configureDelegate">    A function delegate that yields a Database. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="optionsMonitor">       The options monitor. </param>
    protected ConfigurableNMemoryDataProvider(Func<Database> configureDelegate, TDataService dataService,
        IOptionsMonitor<DataOptions> optionsMonitor) : base(dataService, optionsMonitor)
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