using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>   Basic, abstract implementation of an IDataService. </summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class DataService<TUnitOfWork> : IDataService<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected DataService(ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory; // we allow null here
            Logger = LoggerFactory?.CreateLogger<DataService<TUnitOfWork>>();
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

        /// <summary>   Gets a value indicating whether the supports migration. </summary>
        /// <value> True if supports migration, false if not. </value>
        public abstract bool SupportsMigration { get; }

        /// <summary>   Gets the migrator. </summary>
        /// <returns>   The migrator. </returns>
        public abstract IDataMigrator GetMigrator();

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>Begins unit of work.</summary>
        /// <returns>An IUnitOfWork.</returns>
        public abstract TUnitOfWork BeginUnitOfWork();

        /// <summary>Begins unit of work.</summary>
        /// <param name="other">The other.</param>
        /// <returns>An IUnitOfWork.</returns>
        public abstract TUnitOfWork BeginUnitOfWork(IUnitOfWork other);

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