using System;
using System.IO;
using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql tests.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");

            if (!string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(usr))
            {
                ServiceOptions = new PgsqlDapperServiceOptions
                {
                    ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
                };

                DataService = new PgsqlTestDataService(ServiceOptions, null);
            }
            else
            {
                try
                {
                    var path = DirectoryHelper.GetApplicationRoot();
                    var parent = Directory.GetParent(path)?.Parent?.Parent?.FullName;
                    var config = new ConfigurationBuilder()
                        .SetBasePath(parent)
                        .AddJsonFile("appsettings.integration.json", false, true)
                        .AddJsonFile("appsettings.integration.secret.json", true, true)
                        .Build();

                    var manager = new ConfigurationManager(config);
                    var pgsqlOptions = manager.ExtractSettings<PgsqlDapperServiceOptions>();

                    ServiceOptions = new PgsqlDapperServiceOptions
                    {
                        ConnectionString = pgsqlOptions.ConnectionString
                    };
                    DataService = new PgsqlTestDataService(ServiceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }
    }
}