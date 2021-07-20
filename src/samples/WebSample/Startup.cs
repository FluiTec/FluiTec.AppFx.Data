using System;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebSample.Data;
using WebSample.Data.LiteDb;
using WebSample.Data.Mssql;
using WebSample.Data.Mysql;
using WebSample.Data.Pgsql;

namespace WebSample
{
    public class Startup
    {
        #region Properties

        /// <summary>	Gets the configuration. </summary>
        /// <value>	The configuration. </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>   Gets the manager for configuration. </summary>
        /// <value> The configuration manager. </value>
        public ConfigurationManager ConfigurationManager { get; }

        /// <summary>Gets the environment.</summary>
        /// <value>The environment.</value>
        public IWebHostEnvironment Environment { get; }

        #endregion

        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="environment">  The environment. </param>
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.secret.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfigurationManager = new ConsoleReportingConfigurationManager(Configuration);
        }

        #endregion

        #region Services

        /// <summary>   Configure ASP net core. </summary>
        /// <param name="services"> The services. </param>
        private void ConfigureAspNetCore(IServiceCollection services)
        {
            services.ConfigureDynamicDataProvider(ConfigurationManager,
                new Func<DynamicDataOptions, IServiceProvider, ITestDataService>((options, provider) =>
                    {
                        return options.Provider switch
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
        }

        #endregion

        #region AspNetCore

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAspNetCore(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var dataService = context.RequestServices.GetRequiredService<ITestDataService>() as IDapperDataService;
                    await context.Response.WriteAsync(dataService?.ConnectionString ?? "no valid connection-string");
                });
            });
        }

        #endregion
    }
}