﻿using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dynamic.Helpers
{
    /// <summary>   A mssql helper. </summary>
    internal static class MssqlHelper
    {
        /// <summary>   Options for controlling the operation. </summary>
        private static MssqlDapperServiceOptions _options;

        /// <summary>   Provide service. </summary>
        /// <typeparam name="TDataService"> Type of the data service. </typeparam>
        /// <param name="services">             The services. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <param name="provider">             The provider. </param>
        /// <returns>   A TDataService. </returns>
        internal static TDataService ProvideService<TDataService>(IServiceCollection services, ConfigurationManager configurationManager, DynamicDataProvider<TDataService> provider, ILoggerFactory loggerFactory)
        {
            if (_options != null) return provider.ProvideUsingMssql(_options, loggerFactory);

            _options = services.Configure<MssqlDapperServiceOptions>(configurationManager);
            if (configurationManager is ValidatingConfigurationManager validatingConfigurationManager)
            {
                validatingConfigurationManager.ConfigureValidator(new DapperServiceOptionsValidator());
            }

            return provider.ProvideUsingMssql(_options, loggerFactory);
        }
    }
}