﻿using System;
using System.Collections.Generic;
using DynamicSample.Data;
using DynamicSample.Data.LiteDb;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DynamicSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Starting here: general config that is normally handled by ASP.NET
            var configValues = new List<KeyValuePair<string, string>>(new[]
            {
                new KeyValuePair<string, string>("DynamicDataOptions:Provider", "LiteDb"),
                new KeyValuePair<string, string>("LiteDb:DbFileName", "test.ldb"), 
                new KeyValuePair<string, string>("LiteDb:ApplicationFolder", "C:\\dev\\GitLab"), 
                new KeyValuePair<string, string>("LiteDb:UseSingletonConnection", "true") 
            });

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();

            var manager = new ConsoleReportingConfigurationManager(config);
            var services = new ServiceCollection();

            // Starting here: actual config required to use dynamic data
            services.ConfigureDynamicDataProvider(manager, new Func<IServiceProvider,ITestDataService>(provider =>
            {
                var dynamicOptions = provider.GetRequiredService<DynamicDataOptions>();
                return dynamicOptions.Provider switch
                {
                    DataProvider.LiteDb => new LiteDbTestDataService(
                        provider.GetRequiredService<LiteDbServiceOptions>(), provider.GetService<ILoggerFactory>()),
                    // DataProvider.Mssql => expr,
                    _ => throw new NotImplementedException()
                };
            }));

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ITestDataService>();
        }
    }
}
