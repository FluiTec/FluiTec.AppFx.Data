using System;
using System.Collections.Generic;
using DynamicSample.Data;
using DynamicSample.Data.Entities;
using DynamicSample.Data.LiteDb;
using DynamicSample.Data.Mssql;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DynamicSample
{
    internal class Program
    {
        private static void Main()
        {
            var configValues = new List<KeyValuePair<string, string>>(new[]
            {
                new KeyValuePair<string, string>("DynamicDataOptions:Provider", "Mssql"),
                new KeyValuePair<string, string>("DynamicDataOptions:AutoMigrate", "true"),
                new KeyValuePair<string, string>("LiteDb:DbFileName", "test.ldb"), 
                new KeyValuePair<string, string>("LiteDb:ApplicationFolder", "C:\\dev\\GitLab"), 
                new KeyValuePair<string, string>("LiteDb:UseSingletonConnection", "true"),
                new KeyValuePair<string, string>("Dapper.Mssql:ConnectionString", "Data Source=DB1;Initial Catalog=Wtschnell;Integrated Security=False;User ID=appfx;Password=appfx;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"), 
            });

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
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
                new Func<DynamicDataOptions, IServiceProvider, ITestDataService>((options, provider) =>
                    {
                        return options.Provider switch
                        {
                            DataProvider.LiteDb => new LiteDbTestDataService(provider.GetRequiredService<LiteDbServiceOptions>(), provider.GetService<ILoggerFactory>()),
                            DataProvider.Mssql => new MssqlTestDataService(provider.GetRequiredService<MssqlDapperServiceOptions>(), provider.GetService<ILoggerFactory>()),
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
                new Func<DynamicDataOptions, IServiceProvider, ITestDataService>((options, provider) =>
                    {
                        return options.Provider switch
                        {
                            DataProvider.LiteDb => new LiteDbTestDataService(provider.GetRequiredService<LiteDbServiceOptions>(), provider.GetService<ILoggerFactory>()),
                            DataProvider.Mssql => new MssqlTestDataService(provider.GetRequiredService<MssqlDapperServiceOptions>(), provider.GetService<ILoggerFactory>()),
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
