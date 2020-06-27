using System;
using System.IO;
using System.Runtime.InteropServices;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.DataServices
{
    /// <summary>   A service for accessing base lite database data information.</summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class BaseLiteDbDataService<TUnitOfWork> : DataService<TUnitOfWork>, ILiteDbDataService
        where TUnitOfWork : LiteDbUnitOfWork, IUnitOfWork
    {
        #region Fields

        /// <summary>	Gets a value indicating whether this object use singleton connection. </summary>
        /// <value>	True if use singleton connection, false if not. </value>
        private readonly bool _useSingletonConnection;

        #endregion

        #region Methods

        /// <summary>	Gets the filename of the construct application data database file. </summary>
        /// <exception cref="NotSupportedException">
        ///     Thrown when the requested operation is not
        ///     supported.
        /// </exception>
        /// <param name="applicationFolder">	Pathname of the application folder. </param>
        /// <param name="fileName">				Filename of the file. </param>
        /// <returns>	The filename of the construct application data database file. </returns>
        protected virtual string ConstructAppDataDbFileName(string applicationFolder, string fileName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var appData = Environment.GetEnvironmentVariable("LocalAppData");
                return Path.Combine(appData ?? throw new InvalidOperationException(), applicationFolder, fileName);
            }

            // reason: leave open for os x
            // ReSharper disable once InvertIf
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var appData = Environment.GetEnvironmentVariable("user.home");
                return Path.Combine(appData ?? throw new InvalidOperationException(), applicationFolder, fileName);
            }

            // TODO: Implement method for os x

            throw new NotSupportedException("Operating-System is not supported.");
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
            Database?.Dispose();
            Database = null;
        }

        #endregion

        #region Properties

        /// <summary>	Gets or sets the database. </summary>
        /// <value>	The database. </value>
        public LiteDatabase Database { get; private set; }

        /// <summary>   Gets or sets the name service. </summary>
        /// <value> The name service. </value>
        public IEntityNameService NameService { get; }

        #endregion

        #region Constructors

        /// <summary>   Specialised constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isnt rooted or doesnt start with a dot - an applicationFolder is required,
        ///     because the service will save in local-appdata.
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

        /// <summary>   Specialised constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isnt rooted or doesnt start with a dot - an applicationFolder is required,
        ///     because the service will save in local-appdata.
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

            if (!Path.IsPathRooted(dbFilePath) && !dbFilePath.StartsWith("."))
                if (string.IsNullOrWhiteSpace(applicationFolder))
                    throw new ArgumentException(
                        $"Giving non-rooted {nameof(dbFilePath)} requires giving an {nameof(applicationFolder)}.");

            _useSingletonConnection = useSingletonConnection ?? false;

            Database = _useSingletonConnection
                ? LiteDbDatabaseSingleton.GetDatabase(fullDbFilePath)
                : new LiteDatabase(fullDbFilePath);
        }

        /// <summary>   Specialised constructor for use only by derived class. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        protected BaseLiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : this(
            options?.UseSingletonConnection,
            options?.DbFileName, loggerFactory, options?.ApplicationFolder)
        {
        }

        /// <summary>   Specialised constructor for use only by derived class. </summary>
        /// <remarks>
        ///     If dbFilePath isnt rooted or doesnt start with a dot - an applicationFolder is required,
        ///     because the service will save in local-appdata.
        /// </remarks>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        protected BaseLiteDbDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory,
            IEntityNameService nameService) : this(options?.UseSingletonConnection,
            options?.DbFileName, loggerFactory, nameService, options?.ApplicationFolder)
        {
        }

        #endregion
    }
}