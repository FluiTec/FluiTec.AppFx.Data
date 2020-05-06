using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dynamic.Helpers
{
    /// <summary>   A lite database helper. </summary>
    internal static class LiteDbHelper
    {
        /// <summary>   Options for controlling the operation. </summary>
        private static LiteDbServiceOptions _options;

        /// <summary>   Provide service. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="provider">             The provider. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        /// <returns>   A TDataService. </returns>
        internal static TDataService ProvideService<TDataService>(IServiceCollection services, ConfigurationManager configurationManager, DynamicDataProvider<TDataService> provider, ILoggerFactory loggerFactory)
        {
            if (_options != null) return provider.ProvideUsingLiteDb(_options, loggerFactory);

            _options = services.Configure<LiteDbServiceOptions>(configurationManager);
            if (configurationManager is ValidatingConfigurationManager validatingConfigurationManager)
            {
                validatingConfigurationManager.ConfigureValidator(new DapperServiceOptionsValidator());
            }

            return provider.ProvideUsingLiteDb(_options, loggerFactory);
        }
    }
}