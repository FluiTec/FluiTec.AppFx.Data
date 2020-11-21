using System;
using System.IO;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.TestLibrary.Configuration;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var pw = Environment.GetEnvironmentVariable("SA_PASSWORD");

            MssqlDapperServiceOptions serviceOptions = null;
            MssqlTestDataService dataService = null;

            if (!string.IsNullOrWhiteSpace(pw))
            {
                serviceOptions = new MssqlDapperServiceOptions
                {
                    ConnectionString =
                        $"Data Source=microsoft-mssql-server-linux;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={pw};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                };

                dataService = new MssqlTestDataService(serviceOptions, null);
            }
            else
            {
                try
                {
                    var path = DirectoryHelper.GetApplicationRoot();
                    var parent = Directory.GetParent(path).Parent?.Parent?.FullName;
                    var config = new ConfigurationBuilder()
                        .SetBasePath(parent)
                        .AddJsonFile("appsettings.integration.json", false, true)
                        .AddJsonFile("appsettings.integration.secret.json", true, true)
                        .Build();

                    var manager = new ConfigurationManager(config);
                    var options = manager.ExtractSettings<MssqlAdminOption>();
                    var mssqlOptions = manager.ExtractSettings<MssqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(mssqlOptions.ConnectionString)) return;

                    MssqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    MssqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    serviceOptions = new MssqlDapperServiceOptions
                    {
                        ConnectionString = mssqlOptions.ConnectionString
                    };
                    dataService = new MssqlTestDataService(serviceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            if (serviceOptions == null || dataService == null) return;

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] {dataService.GetType().Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate();
        }
    }
}