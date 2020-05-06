using System;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>	Interface for a data service. </summary>
    public interface IDataService : IDisposable
    {
        /// <summary>Gets the logger factory.</summary>
        /// <value>The logger factory.</value>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>	Gets the name. </summary>
        /// <value>	The name. </value>
        string Name { get; }
    }

    /// <summary>   Interface for data service. </summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public interface IDataService<out TUnitOfWork> : IDataService
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>   Begins unit of work. </summary>
        /// <returns>   A TUnitOfWork. </returns>
        TUnitOfWork BeginUnitOfWork();

        /// <summary>   Begins unit of work. </summary>
        /// <param name="other">    The other. </param>
        /// <returns>   A TUnitOfWork. </returns>
        TUnitOfWork BeginUnitOfWork(IUnitOfWork other);
    }
}