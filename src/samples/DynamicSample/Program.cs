using System;
using DynamicSample.Data;
using DynamicSample.Data.Entities;
using DynamicSample.Data.LiteDb;
using DynamicSample.Data.Mssql;
using DynamicSample.Data.Mysql;
using DynamicSample.Data.NMemory;
using DynamicSample.Data.Pgsql;
using DynamicSample.Data.Sqlite;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Programs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DynamicSample;

internal class Program : ValidatingConfigurationManagerProgram
{
    /// <summary>   Main entry-point for this application. </summary>
    /// <param name="args"> A variable-length parameters list containing arguments. </param>
    // ReSharper disable once UnusedParameter.Local
    private static void Main(params string[] args)
    {
        var prog = new Program();
        var sp = prog.GetServiceProvider();

        prog.TestDynamicSql(sp);
        prog.TestTimeStamp(sp);
    }

    /// <summary>
    ///     Tests dynamic SQL.
    /// </summary>
    /// <param name="serviceProvider">  The service provider. </param>
    private void TestDynamicSql(IServiceProvider serviceProvider)
    {
        Console.WriteLine($"METHOD: {nameof(TestDynamicSql)}");

        var service = serviceProvider.GetRequiredService<ITestDataService>();
        using (var uow = service.BeginUnitOfWork())
        {
            uow.DummyRepository.Add(new DummyEntity {Name = "Testname"});
            uow.Commit();
        }

        Console.WriteLine($"{service.Name}");
    }

    /// <summary>
    ///     Tests time stamp.
    /// </summary>
    /// <param name="serviceProvider">  The service provider. </param>
    private void TestTimeStamp(IServiceProvider serviceProvider)
    {
        Console.WriteLine($"METHOD: {nameof(TestTimeStamp)}");

        var service = serviceProvider.GetRequiredService<ITestDataService>();

        using (var uow = service.BeginUnitOfWork())
        {
            uow.Dummy2Repository.Add(new DummyEntity2 {Name = "Testname"});
            uow.Commit();
        }

        using (var uow = service.BeginUnitOfWork())
        {
            var entities = uow.Dummy2Repository.GetAll();
            foreach (var entity in entities)
            {
                entity.Name = "Test2";
                uow.Dummy2Repository.Update(entity);
            }

            uow.Commit();
        }
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
            .AddJsonFile("appsettings.secret.json", false, true);
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

        return services;
    }
}