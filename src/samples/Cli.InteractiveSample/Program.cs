using System;
using Cli.InteractiveSample.Data;
using Cli.InteractiveSample.Data.LiteDb;
using Cli.InteractiveSample.Data.Mssql;
using Cli.InteractiveSample.Data.Mysql;
using Cli.InteractiveSample.Data.Pgsql;
using FluiTec.AppFx.Console;
using FluiTec.AppFx.Console.Configuration;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.Dynamic.Console;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Console;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.InteractiveSample
{
    /// <summary>
    ///     A program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     Main entry-point for this application.
        /// </summary>
        /// <param name="args"> An array of command-line argument strings. </param>
        private static void Main(string[] args)
        {
            var config = BuildConfiguration();
            var serviceProvider = ConfigureServices(config);

            new ConsoleHost(serviceProvider).RunInteractive("Test", args);
        }

        /// <summary>
        ///     Builds the configuration.
        /// </summary>
        /// <returns>
        ///     An IConfigurationRoot.
        /// </returns>
        private static IConfigurationRoot BuildConfiguration()
        {
            // load config from json
            var path = DirectoryHelper.GetApplicationRoot();
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.secret.json", false, true)
                .AddSaveableJsonFile("appsettings.conf.json", false, true)
                .Build();

            return config;
        }

        /// <summary>
        ///     Configure services.
        /// </summary>
        /// <param name="config">   The configuration. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        private static IServiceProvider ConfigureServices(IConfigurationRoot config)
        {
            var manager = new ValidatingConfigurationManager(config);
            var services = new ServiceCollection();

            services.AddSingleton(config);
            services.ConfigureDynamicDataProvider(manager,
                new Func<IOptionsMonitor<DynamicDataOptions>, IServiceProvider, ITestDataService>((options, provider) =>
                    {
                        return options.CurrentValue.Provider switch
                        {
                            DataProvider.LiteDb => new LiteDbTestDataService(
                                provider.GetRequiredService<LiteDbServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Mssql => new MssqlTestDataService(
                                provider.GetRequiredService<IOptionsMonitor<MssqlDapperServiceOptions>>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Pgsql => new PgsqlTestDataService(
                                provider.GetRequiredService<IOptionsMonitor<PgsqlDapperServiceOptions>>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Mysql => new MysqlTestDataService(
                                provider.GetRequiredService<IOptionsMonitor<MysqlDapperServiceOptions>>(),
                                provider.GetService<ILoggerFactory>()),
                            _ => throw new NotImplementedException()
                        };
                    }
                )
            );

            services.ConfigureOptionsConsoleModule();
            services.ConfigureDataConsoleModule();

            return services.BuildServiceProvider();
        }
    }
}