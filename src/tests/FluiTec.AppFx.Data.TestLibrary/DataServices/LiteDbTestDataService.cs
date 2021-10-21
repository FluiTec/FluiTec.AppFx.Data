using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.TestLibrary.DataServices
{
    /// <summary>
    /// A service for accessing lite database test data information.
    /// </summary>
    public class LiteDbTestDataService : LiteDbDataService<LiteDbTestUnitOfWork>, ITestDataService
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public override string Name => nameof(LiteDbTestDataService);

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        public LiteDbTestDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory, string applicationFolder = null) : base(useSingletonConnection, dbFilePath, loggerFactory, applicationFolder)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="nameService">              The name service. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        public LiteDbTestDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory, IEntityNameService nameService, string applicationFolder = null) : base(useSingletonConnection, dbFilePath, loggerFactory, nameService, applicationFolder)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public LiteDbTestDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        public LiteDbTestDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory, IEntityNameService nameService) : base(options, loggerFactory, nameService)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public LiteDbTestDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        public LiteDbTestDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory, IEntityNameService nameService) : base(options, loggerFactory, nameService)
        {
        }
        
        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override LiteDbTestUnitOfWork BeginUnitOfWork()
        {
            return new LiteDbTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <param name="other">    The other. </param>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override LiteDbTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is LiteDbUnitOfWork work))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(LiteDbUnitOfWork)}!");
            return new LiteDbTestUnitOfWork(this, work, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <param name="other">    The other. </param>
        /// 
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }
    }
}