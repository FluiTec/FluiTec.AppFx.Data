using System;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.DataServices
{
    /// <summary>   A service for accessing dapper data information. </summary>
    public abstract class DapperDataService : DataService
    {
        #region Properties

        /// <summary>	Gets or sets the connection string. </summary>
        /// <value>	The connection string. </value>
        public string ConnectionString { get; protected set; }

        /// <summary>	Gets or sets the connection factory. </summary>
        /// <value>	The connection factory. </value>
        public IConnectionFactory ConnectionFactory { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="logger">               The logger. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected DapperDataService(IDapperServiceOptions dapperServiceOptions,
            ILogger<IDataService> logger, ILoggerFactory loggerFactory)
            : base(logger, loggerFactory)
        {
            if (dapperServiceOptions == null) throw new ArgumentNullException(nameof(dapperServiceOptions));
            ConnectionString = dapperServiceOptions.ConnectionString;
            ConnectionFactory = dapperServiceOptions.ConnectionFactory;
        }

        #endregion

        #region IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            // nothing to do here
        }

        #endregion

        #region IDataService

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   An IUnitOfWork. </returns>
        public override IUnitOfWork BeginUnitOfWork()
        {
            return new DapperUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work. </summary>
        /// <param name="other">    The other. </param>
        /// <returns>   An IUnitOfWork. </returns>
        public override IUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is DapperUnitOfWork))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(DapperUnitOfWork)}!");
            return new DapperUnitOfWork((DapperUnitOfWork) other, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        #endregion
    }
}