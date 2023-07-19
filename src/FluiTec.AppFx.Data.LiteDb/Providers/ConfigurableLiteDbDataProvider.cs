using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Providers;

/// <summary>   A configurable lite database data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public abstract class ConfigurableLiteDbDataProvider<TDataService, TUnitOfWork> : LiteDbDataProvider<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="configureDelegate">    A function delegate that yields a Database. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="options">              Options for controlling the operation. </param>
    protected ConfigurableLiteDbDataProvider(Func<LiteDatabase> configureDelegate, TDataService dataService, DataOptions options) : base(dataService, options)
    {
        ConfigureDelegate = configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate));
    }

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="configureDelegate">    A function delegate that yields a Database. </param>
    /// <param name="dataService">          The data service. </param>
    /// <param name="optionsMonitor">       The options monitor. </param>
    protected ConfigurableLiteDbDataProvider(Func<LiteDatabase> configureDelegate, TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor) : base(dataService, optionsMonitor)
    {
        ConfigureDelegate = configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate));
    }

    /// <summary>   Gets the configure delegate. </summary>
    /// <value> A function delegate that yields a Database. </value>
    public Func<LiteDatabase> ConfigureDelegate { get; }
    
    /// <summary>   Configure database. </summary>
    /// <returns>   A Database. </returns>
    protected override LiteDatabase ConfigureDatabase()
    {
        return ConfigureDelegate();
    }
}