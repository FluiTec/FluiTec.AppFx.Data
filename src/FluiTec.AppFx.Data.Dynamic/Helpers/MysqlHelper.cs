using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dynamic.Helpers
{
    /// <summary>   A mysql helper. </summary>
    internal static class MysqlHelper
    {
        /// <summary>   Options for controlling the operation. </summary>
        private static MysqlDapperServiceOptions _options;

        /// <summary>   Provide service. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="provider">             The provider. </param>
        /// <returns>   A TDataService. </returns>
        internal static TDataService ProvideService<TDataService>(IServiceCollection services, ConfigurationManager configurationManager, DynamicDataProvider<TDataService> provider)
        {
            if (_options != null) return provider.ProvideUsingMysql(_options);

            _options = services.Configure<MysqlDapperServiceOptions>(configurationManager);
            if (configurationManager is ValidatingConfigurationManager validatingConfigurationManager)
            {
                validatingConfigurationManager.ConfigureValidator(new DapperServiceOptionsValidator());
            }

            return provider.ProvideUsingMysql(_options);
        }
    }
}