using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>   A dynamic data configuration extension. </summary>
    public static class DynamicDataConfigurationExtension
    {
        /// <summary>   Configure dynamic data provider. </summary>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection ConfigureDynamicDataProvider(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            // provider-selection is required
            services.Configure<DynamicDataOptions>(configurationManager, true);

            // provider-configurations are optional (at least here)
            services.Configure<LiteDbServiceOptions>(configurationManager);
            services.Configure<MssqlDapperServiceOptions>(configurationManager);
            services.Configure<MysqlDapperServiceOptions>(configurationManager);
            services.Configure<PgsqlDapperServiceOptions>(configurationManager);
            services.Configure<SqliteDapperServiceOptions>(configurationManager);

            return services;
        }
    }
}