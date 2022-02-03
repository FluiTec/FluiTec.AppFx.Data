using System;
using Cli.InteractiveSample.Data;
using Cli.InteractiveSample.Data.LiteDb;
using Cli.InteractiveSample.Data.Mssql;
using Cli.InteractiveSample.Data.Mysql;
using Cli.InteractiveSample.Data.Pgsql;
using Cli.InteractiveSample.Data.Sqlite;
using FluiTec.AppFx.Console;
using FluiTec.AppFx.Console.Configuration;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.Dynamic.Console;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Console;
using FluiTec.AppFx.Options.Programs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cli.InteractiveSample;

/// <summary>
///     A program.
/// </summary>
internal class Program : ValidatingConfigurationManagerProgram
{
    /// <summary>
    ///     Main entry-point for this application.
    /// </summary>
    /// <param name="args"> An array of command-line argument strings. </param>
    private static void Main(string[] args)
    {
        var sp = new Program().GetServiceProvider();
        new ConsoleHost(sp).RunInteractive("Test", args);
    }

    /// <summary>
    ///     Configures the given configuration builder.
    /// </summary>
    /// <param name="configurationBuilder"> The configuration builder. </param>
    /// <returns>
    ///     An IConfigurationBuilder.
    /// </returns>
    protected override IConfigurationBuilder Configure(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.secret.json", false, true)
            .AddSaveableJsonFile("appsettings.conf.json", false, true);
    }

    /// <summary>
    ///     Configure services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <returns>
    ///     A ServiceCollection.
    /// </returns>
    protected override ServiceCollection ConfigureServices(ServiceCollection services)
    {
        base.ConfigureServices(services);

        services.ConfigureDynamicDataProvider<ITestDataService, DynamicDataOptions>(Manager, (options, provider) =>
        {
            return options.CurrentValue.Provider switch
            {
                DataProvider.LiteDb => new LiteDbTestDataService(
                    provider.GetRequiredService<IOptionsMonitor<LiteDbServiceOptions>>(),
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
                DataProvider.Sqlite => new SqliteTestDataService(
                    provider.GetRequiredService<IOptionsMonitor<SqliteDapperServiceOptions>>(),
                    provider.GetService<ILoggerFactory>()),
                _ => throw new NotImplementedException()
            };
        });

        //services.ConfigureDynamicDataProvider(Manager,
        //    new Func<IOptionsMonitor<DynamicDataOptions>, IServiceProvider, ITestDataService>((options, provider) =>
        //        {
        //            return options.CurrentValue.Provider switch
        //            {
        //                DataProvider.LiteDb => new LiteDbTestDataService(
        //                    provider.GetRequiredService<IOptionsMonitor<LiteDbServiceOptions>>(),
        //                    provider.GetService<ILoggerFactory>()),
        //                DataProvider.Mssql => new MssqlTestDataService(
        //                    provider.GetRequiredService<IOptionsMonitor<MssqlDapperServiceOptions>>(),
        //                    provider.GetService<ILoggerFactory>()),
        //                DataProvider.Pgsql => new PgsqlTestDataService(
        //                    provider.GetRequiredService<IOptionsMonitor<PgsqlDapperServiceOptions>>(),
        //                    provider.GetService<ILoggerFactory>()),
        //                DataProvider.Mysql => new MysqlTestDataService(
        //                    provider.GetRequiredService<IOptionsMonitor<MysqlDapperServiceOptions>>(),
        //                    provider.GetService<ILoggerFactory>()),
        //                DataProvider.Sqlite => new SqliteTestDataService(
        //                    provider.GetRequiredService<IOptionsMonitor<SqliteDapperServiceOptions>>(),
        //                    provider.GetService<ILoggerFactory>()),
        //                _ => throw new NotImplementedException()
        //            };
        //        }
        //    )
        //);

        services.ConfigureOptionsConsoleModule();
        services.ConfigureDataConsoleModule();

        return services;
    }
}