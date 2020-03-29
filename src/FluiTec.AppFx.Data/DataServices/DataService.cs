using System;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices
{
    /// <summary>Basic, abstract implementation of an IDataService.</summary>
    /// <seealso cref="FluiTec.AppFx.Data.DataServices.IDataService" />
    public abstract class DataService : IDataService
    {
        /// <summary>
        ///     <para>Initializes a new instance of the <see cref="DataService" /> class.</para>
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="loggerFactory">The loggerFactory.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        protected DataService(string name, ILogger<IDataService> logger, ILoggerFactory loggerFactory)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} must not be null or whitespace.");
            Name = name;
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
        public virtual string Name { get; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();

        /// <summary>Begins unit of work.</summary>
        /// <returns>An IUnitOfWork.</returns>
        public abstract IUnitOfWork BeginUnitOfWork();

        /// <summary>Begins unit of work.</summary>
        /// <param name="other">The other.</param>
        /// <returns>An IUnitOfWork.</returns>
        public abstract IUnitOfWork BeginUnitOfWork(IUnitOfWork other);
    }
}