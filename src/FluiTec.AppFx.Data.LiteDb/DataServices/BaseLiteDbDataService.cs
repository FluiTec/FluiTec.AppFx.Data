using System;
using System.Collections.Generic;
using System.IO;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.DataServices
{
    /// <summary>   A service for accessing base lite database data information.</summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class BaseLiteDbDataService<TUnitOfWork> : DataService<TUnitOfWork>, ILiteDbDataService
        where TUnitOfWork : LiteDbUnitOfWork, IUnitOfWork
    {
        #region Methods

        /// <summary>
        ///     Gets cached database.
        /// </summary>
        /// <param name="options">  Options for controlling the operation. </param>
        /// <returns>
        ///     The cached database.
        /// </returns>
        private LiteDatabase GetCachedDatabase(LiteDbServiceOptions options)
        {
            var key = options.FullDbFilePath.GetHashCode();

            if (_databases.ContainsKey(key))
                return _databases[key];

            var db = new LiteDatabase(options.FullDbFilePath);
            _databases.Add(key, db);
            return db;
        }

        #endregion

        #region IDisposable

        /// <summary>
        ///     Releases the unmanaged resources used by the FluiTec.AppFx.Data.Dapper.DapperDataService and
        ///     optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (_useSingletonConnection) return;
            _database?.Dispose();
            _database = null;
        }

        #endregion

        #region Fields

        /// <summary>	Gets a value indicating whether this object use singleton connection. </summary>
        /// <value>	True if use singleton connection, false if not. </value>
        private readonly bool _useSingletonConnection;

        /// <summary>
        ///     The database.
        /// </summary>
        private LiteDatabase _database;

        /// <summary>
        ///     (Immutable) the databases.
        /// </summary>
        private readonly Dictionary<int, LiteDatabase> _databases = new Dictionary<int, LiteDatabase>();

        #endregion

        #region Properties

        /// <summary>   Gets options for controlling the service. </summary>
        /// <value> Options that control the service. </value>
        protected IOptionsMonitor<LiteDbServiceOptions> ServiceOptions { get; }

        /// <summary>	Gets or sets the database. </summary>
        /// <value>	The database. </value>
        public LiteDatabase Database =>
            ServiceOptions == null
                ? _database
                : ServiceOptions.CurrentValue.UseSingletonConnection
                    ? LiteDbDatabaseSingleton.GetDatabase(ServiceOptions.CurrentValue.FullDbFilePath)
                    : GetCachedDatabase(ServiceOptions.CurrentValue);

        /// <summary>   Gets or sets the name service. </summary>
        /// <value> The name service. </value>
        public IEntityNameService NameService { get; }

        #endregion

        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isn't rooted or doesn't start with a dot - an applicationFolder is required,
        ///     because the service will save in local app data.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="useSingletonConnection">   True to use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        protected BaseLiteDbDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory,
            string applicationFolder = null) :
            this(useSingletonConnection, dbFilePath, loggerFactory, new AttributeEntityNameService(), applicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isn't rooted or doesn't start with a dot - an applicationFolder is required,
        ///     because the service will save in local app data.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="useSingletonConnection">   True to use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="nameService">              The name service. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        protected BaseLiteDbDataService(bool? useSingletonConnection, string dbFilePath,
            ILoggerFactory loggerFactory, IEntityNameService nameService, string applicationFolder = null) :
            base(loggerFactory)
        {
            NameService = nameService ?? throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(dbFilePath)) throw new ArgumentNullException(nameof(dbFilePath));

            var fullDbFilePath = applicationFolder != null ? Path.Combine(applicationFolder, dbFilePath) : dbFilePath;

            _useSingletonConnection = useSingletonConnection ?? false;
            _database = _useSingletonConnection
                ? LiteDbDatabaseSingleton.GetDatabase(fullDbFilePath)
                : new LiteDatabase(fullDbFilePath);
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected BaseLiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : this(
            options?.UseSingletonConnection,
            options?.DbFileName, loggerFactory, options?.ApplicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isn't rooted or doesn't start with a dot - an applicationFolder is required,
        ///     because the service will save in local app data.
        /// </remarks>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        protected BaseLiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory,
            IEntityNameService nameService) : this(options?.UseSingletonConnection,
            options?.DbFileName, loggerFactory, nameService, options?.ApplicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected BaseLiteDbDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory) :
            this(
                options?.CurrentValue.UseSingletonConnection,
                options?.CurrentValue.DbFileName, loggerFactory, options?.CurrentValue.ApplicationFolder)
        {
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isn't rooted or doesn't start with a dot - an applicationFolder is required,
        ///     because the service will save in local app data.
        /// </remarks>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        protected BaseLiteDbDataService(IOptionsMonitor<LiteDbServiceOptions> options, ILoggerFactory loggerFactory,
            IEntityNameService nameService) : this(options?.CurrentValue.UseSingletonConnection,
            options?.CurrentValue.DbFileName, loggerFactory, nameService, options?.CurrentValue.ApplicationFolder)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            if (options.CurrentValue == null)
                throw new ArgumentException("Missing current value.", nameof(options));
            if (string.IsNullOrWhiteSpace(options.CurrentValue.DbFileName))
                throw new ArgumentException("Invalid DbFileName.", nameof(options));

            ServiceOptions = options;
            NameService = nameService ?? throw new ArgumentNullException();
        }

        #endregion
    }
}