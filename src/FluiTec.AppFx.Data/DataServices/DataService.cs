using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Basic, abstract implementation of an IDataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.IDataService" />
    public abstract class DataService : IDataService
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="logger">           The logger. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected DataService(ILogger<IDataService> logger, ILoggerFactory loggerFactory)
        {
            Logger = logger; // we allow null here
            LoggerFactory = loggerFactory; // we allow null here
        }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        public ILogger<IDataService> Logger { get; }

        /// <summary>Gets the logger factory.</summary>
        /// <value>The logger factory.</value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>Begins unit of work.</summary>
        /// <returns>An IUnitOfWork.</returns>
        public abstract IUnitOfWork BeginUnitOfWork();

        /// <summary>Begins unit of work.</summary>
        /// <param name="other">The other.</param>
        /// <returns>An IUnitOfWork.</returns>
        public abstract IUnitOfWork BeginUnitOfWork(IUnitOfWork other);

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected abstract void Dispose(bool disposing);
    }
}