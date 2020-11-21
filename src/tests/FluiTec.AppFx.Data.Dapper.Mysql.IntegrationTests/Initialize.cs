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

namespace FluiTec.AppFx.Data.Dapper.Mysql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var pw = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            MysqlDapperServiceOptions serviceOptions = null;
            MysqlTestDataService dataService = null;

            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(pw))
            {
                serviceOptions = new MysqlDapperServiceOptions
                {
                    ConnectionString = $"Server=mysql;Database={db};Uid=root;Pwd={pw}"
                };

                dataService = new MysqlTestDataService(serviceOptions, null);
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
                    var options = manager.ExtractSettings<MysqlAdminOption>();
                    var mysqlOptions = manager.ExtractSettings<MysqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(mysqlOptions.ConnectionString)) return;

                    MysqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    MysqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    serviceOptions = new MysqlDapperServiceOptions
                    {
                        ConnectionString = mysqlOptions.ConnectionString
                    };
                    dataService = new MysqlTestDataService(serviceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            if (serviceOptions == null || dataService == null) return;

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] {dataService.GetType().Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddMySql5());
            migrator.Migrate();
        }
    }
}