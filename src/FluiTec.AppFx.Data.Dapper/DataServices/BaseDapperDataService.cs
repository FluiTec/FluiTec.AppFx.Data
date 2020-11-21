using System;
using System.Collections.Concurrent;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.DataServices
{
    /// <summary>   A service for accessing base dapper data information. </summary>
    /// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
    public abstract class BaseDapperDataService<TUnitOfWork> : DataService<TUnitOfWork>, IDapperDataService
        where TUnitOfWork : DapperUnitOfWork, IUnitOfWork
    {
        #region Constructors

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected BaseDapperDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) :
            base(loggerFactory)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            if (SqlType == SqlType.Mysql)
                DapperExtensions.InstallDateTimeOffsetMapper();

            if (dapperServiceOptions == null) throw new ArgumentNullException(nameof(dapperServiceOptions));
            ConnectionString = dapperServiceOptions.ConnectionString;
            ConnectionFactory = dapperServiceOptions.ConnectionFactory;
            CommandCache = new ConcurrentDictionary<string, string>();
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

        #region ICommandCache

        /// <summary>   Gets from cache.</summary>
        /// <param name="repositoryType">   Type of the repository. </param>
        /// <param name="memberName">       Name of the member. </param>
        /// <param name="commandFunc">      The command function. </param>
        /// <returns>   The data that was read from the cache.</returns>
        public string GetFromCache(Type repositoryType, string memberName, Func<string> commandFunc)
        {
            var key = $"{repositoryType.FullName}.{memberName}";

            if (CommandCache.TryGetValue(key, out var result))
                return result;

            var cmd = commandFunc();
            CommandCache.TryAdd(key, cmd);
            return cmd;
        }

        #endregion

        #region Properties

        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        public IConnectionFactory ConnectionFactory { get; }

        /// <summary>   Gets the connection string. </summary>
        /// <value> The connection string. </value>
        public string ConnectionString { get; }

        /// <summary>   Gets the command cache.</summary>
        /// <value> The command cache.</value>
        protected ConcurrentDictionary<string, string> CommandCache { get; }

        /// <summary>   Gets information describing the meta. </summary>
        /// <value> Information describing the meta. </value>
        public abstract IVersionTableMetaData MetaData { get; }

        /// <summary>   Gets the schema. </summary>
        /// <value> The schema. </value>
        public abstract string Schema { get; }

        /// <summary>   Gets the type of the SQL. </summary>
        /// <value> The type of the SQL. </value>
        public abstract SqlType SqlType { get; }

        #endregion
    }
}