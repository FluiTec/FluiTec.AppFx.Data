using System;
using DynamicSample.Data;
using DynamicSample.Data.Entities;
using DynamicSample.Data.LiteDb;
using DynamicSample.Data.Mssql;
using DynamicSample.Data.Mysql;
using DynamicSample.Data.Pgsql;
using DynamicSample.Data.Sqlite;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DynamicSample
{
    internal class Program
    {
        /// <summary>   Main entry-point for this application. </summary>
        /// <param name="args"> A variable-length parameters list containing arguments. </param>
        // ReSharper disable once UnusedParameter.Local
        private static void Main(params string[] args)
        {
            // load config from json
            var path = DirectoryHelper.GetApplicationRoot();
            Console.WriteLine($"BasePath: {path}");
            var config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.secret.json", false, true)
                .Build();

            TestDynamicSql(config);
            TestTimeStamp(config);
        }

        private static void TestDynamicSql(IConfigurationRoot config)
        {
            Console.WriteLine($"METHOD: {nameof(TestDynamicSql)}");
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();

            services.ConfigureDynamicDataProvider(manager,
                new Func<IOptionsMonitor<DynamicDataOptions>, IServiceProvider, ITestDataService>((options, provider) =>
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
                    }
                )
            );

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ITestDataService>();
            using (var uow = service.BeginUnitOfWork())
            {
                uow.DummyRepository.Add(new DummyEntity {Name = "Testname"});
                uow.Commit();
            }

            Console.WriteLine($"{service.Name}");
        }

        private static void TestTimeStamp(IConfigurationRoot config)
        {
            Console.WriteLine($"METHOD: {nameof(TestTimeStamp)}");
            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();

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
                            DataProvider.Sqlite => new SqliteTestDataService(
                                provider.GetRequiredService<IOptionsMonitor<SqliteDapperServiceOptions>>(),
                                provider.GetService<ILoggerFactory>()),
                            _ => throw new NotImplementedException()
                        };
                    }
                )
            );

            var serviceProvider = services.BuildServiceProvider();
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
    }
}