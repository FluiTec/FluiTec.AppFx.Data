using System;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices;

/// <summary>	Interface for a data service. </summary>
public interface IDataService : IDisposable
{
    /// <summary>Gets the logger factory.</summary>
    /// <value>The logger factory.</value>
    ILoggerFactory LoggerFactory { get; }

    /// <summary>	Gets the name. </summary>
    /// <value>	The name. </value>
    string Name { get; }

    /// <summary>   Gets a value indicating whether the supports migration. </summary>
    /// <value> True if supports migration, false if not. </value>
    bool SupportsMigration { get; }

    /// <summary>   Gets the migrator. </summary>
    /// <returns>   The migrator. </returns>
    IDataMigrator GetMigrator();
}

/// <summary>   Interface for data service. </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public interface IDataService<out TUnitOfWork> : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    // ReSharper disable once UnusedMemberInSuper.Global
    TUnitOfWork BeginUnitOfWork();

    /// <summary>   Begins unit of work. </summary>
    /// <param name="other">    The other. </param>
    /// <returns>   A TUnitOfWork. </returns>
    // ReSharper disable once UnusedMemberInSuper.Global
    TUnitOfWork BeginUnitOfWork(IUnitOfWork other);
}