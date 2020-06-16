using System;
using System.Collections.Generic;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.DataServices
{
    /// <summary>   A service for accessing dapper data information. </summary>
    public abstract class DapperDataService<TUnitOfWork> : BaseDapperDataService<TUnitOfWork>
        where TUnitOfWork : DapperUnitOfWork, IUnitOfWork
    {
        private IVersionTableMetaData _metaData;

        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected DapperDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory)
            : base(dapperServiceOptions, loggerFactory)
        {
        }

        #endregion

        #region Migration

        /// <summary>   Gets information describing the meta. </summary>
        /// <value> Information describing the meta. </value>
        public override IVersionTableMetaData MetaData => _metaData ?? (_metaData = new VersionTable(Schema));

        /// <summary>   Gets a value indicating whether the supports migration. </summary>
        /// <value> True if supports migration, false if not. </value>
        public override bool SupportsMigration => MetaData != null;

        /// <summary>   Configure SQL type. </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when one or more arguments are outside
        ///     the required range.
        /// </exception>
        /// <returns>   An Action&lt;IMigrationRunnerBuilder&gt; </returns>
        protected virtual Action<IMigrationRunnerBuilder> ConfigureSqlType()
        {
            switch (SqlType)
            {
                case SqlType.Mssql:
                    return rb => rb.AddSqlServer2016();
                case SqlType.Mysql:
                    return rb => rb.AddMySql5();
                case SqlType.Pgsql:
                    return rb => rb.AddPostgres();
                case SqlType.Sqlite:
                    return rb => rb.AddSQLite();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>   Gets the migration assemblies in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the migration assemblies in this
        ///     collection.
        /// </returns>
        /// <remarks>
        ///     Returns the assembly containing the implementing dataservice by default.
        /// </remarks>
        protected virtual IEnumerable<Assembly> GetMigrationAssemblies()
        {
            return new[] {GetType().Assembly};
        }

        /// <summary>   Gets the migrator. </summary>
        /// <returns>   The migrator. </returns>
        public override IDataMigrator GetMigrator()
        {
            return new DapperDataMigrator(ConnectionString, GetMigrationAssemblies(), MetaData, ConfigureSqlType());
        }

        #endregion
    }
}