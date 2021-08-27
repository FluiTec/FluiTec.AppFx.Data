using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.DataServices
{
    /// <summary>   A service for accessing lite database data information.</summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class LiteDbDataService<TUnitOfWork> : BaseLiteDbDataService<TUnitOfWork>
        where TUnitOfWork : LiteDbUnitOfWork, IUnitOfWork
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        protected LiteDbDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory,
            string applicationFolder = null) : base(useSingletonConnection, dbFilePath, loggerFactory,
            applicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="nameService">              The name service. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        protected LiteDbDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory,
            IEntityNameService nameService, string applicationFolder = null) : base(useSingletonConnection, dbFilePath,
            loggerFactory, nameService, applicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected LiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : base(options,
            loggerFactory)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        protected LiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory,
            IEntityNameService nameService) : base(options, loggerFactory, nameService)
        {
        }

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected LiteDbDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory)
            : base(options, loggerFactory)
        {
        }

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        protected LiteDbDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory,
            IEntityNameService nameService) : base(options, loggerFactory, nameService)
        {
        }

        #endregion

        #region Migration

        /// <summary>   Gets a value indicating whether the supports migration. </summary>
        /// <value> True if supports migration, false if not. </value>
        public override bool SupportsMigration => false;

        /// <summary>   Gets the migrator. </summary>
        /// <returns>   The migrator. </returns>
        public override IDataMigrator GetMigrator()
        {
            return null;
        }

        #endregion
    }
}