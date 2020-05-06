using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.Dynamic.Helpers;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dynamic
{
    /// <summary>   A dynamic data provider. </summary>
    /// <typeparam name="TDataService"> Type of the data service. </typeparam>
    public abstract class DynamicDataProvider<TDataService>
    {
        /// <summary>   Options for controlling the operation. </summary>
        protected readonly DynamicDataOptions Options;

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <param name="options">  Options for controlling the operation. </param>
        protected DynamicDataProvider(DynamicDataOptions options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>   Configure data service. </summary>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        public virtual TDataService ProvideDataService(IServiceCollection services, ConfigurationManager configurationManager, ILoggerFactory loggerFactory)
        {
            return Options.Provider switch
            {
                DataProvider.Mssql => MssqlHelper.ProvideService(services, configurationManager, this, loggerFactory),
                DataProvider.Mysql => MysqlHelper.ProvideService(services, configurationManager, this, loggerFactory),
                DataProvider.Pgsql => PgsqlHelper.ProvideService(services, configurationManager, this, loggerFactory),
                DataProvider.Sqlite => SqliteHelper.ProvideService(services, configurationManager, this, loggerFactory),
                DataProvider.LiteDb => LiteDbHelper.ProvideService(services, configurationManager, this, loggerFactory),
                _ => throw new NotImplementedException($"Provider {Options.Provider} was not implemented!")
            };
        }
        
        /// <summary>   Provide using mssql. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        protected internal abstract TDataService ProvideUsingMssql(MssqlDapperServiceOptions options, ILoggerFactory loggerFactory);

        /// <summary>   Provide using mysql. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        protected internal abstract TDataService ProvideUsingMysql(MysqlDapperServiceOptions options, ILoggerFactory loggerFactory);

        /// <summary>   Provide using pgsql. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        protected internal abstract TDataService ProvideUsingPgsql(PgsqlDapperServiceOptions options, ILoggerFactory loggerFactory);

        /// <summary>   Provide using sqlite. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        protected internal abstract TDataService ProvideUsingSqlite(SqliteDapperServiceOptions options, ILoggerFactory loggerFactory);

        /// <summary>   Provide using lite database. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        protected internal abstract TDataService ProvideUsingLiteDb(LiteDbServiceOptions options, ILoggerFactory loggerFactory);
    }
}