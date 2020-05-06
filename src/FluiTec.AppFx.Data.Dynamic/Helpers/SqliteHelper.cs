using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dynamic.Helpers
{
    /// <summary>   A sqlite helper. </summary>
    internal static class SqliteHelper
    {
        /// <summary>   Options for controlling the operation. </summary>
        private static SqliteDapperServiceOptions _options;

        /// <summary>   Provide service. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="provider">             The provider. </param>
        /// <returns>   A TDataService. </returns>
        internal static TDataService ProvideService<TDataService>(IServiceCollection services, ConfigurationManager configurationManager, DynamicDataProvider<TDataService> provider, ILoggerFactory loggerFactory)
        {
            if (_options != null) return provider.ProvideUsingSqlite(_options, loggerFactory);

            _options = services.Configure<SqliteDapperServiceOptions>(configurationManager);
            if (configurationManager is ValidatingConfigurationManager validatingConfigurationManager)
            {
                validatingConfigurationManager.ConfigureValidator(new DapperServiceOptionsValidator());
            }

            return provider.ProvideUsingSqlite(_options, loggerFactory);
        }
    }
}