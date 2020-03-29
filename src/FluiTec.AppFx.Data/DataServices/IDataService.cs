using System;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>	Interface for a data service. </summary>
    public interface IDataService : IDisposable
    {
        ILogger<IDataService> Logger { get; }

        /// <summary>Gets the logger factory.</summary>
        /// <value>The logger factory.</value>
        ILoggerFactory LoggerFactory { get; }

        /// <summary>	Gets the name. </summary>
        /// <value>	The name. </value>
        string Name { get; }

        /// <summary>	Begins unit of work. </summary>
        /// <returns>	An IUnitOfWork. </returns>
        IUnitOfWork BeginUnitOfWork();

        /// <summary>Begins unit of work.</summary>
        /// <param name="other">    The other. </param>
        /// <returns>An IUnitOfWork.</returns>
        IUnitOfWork BeginUnitOfWork(IUnitOfWork other);
    }
}