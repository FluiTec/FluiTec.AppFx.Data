using System;
using System.IO;
using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            var pw = Environment.GetEnvironmentVariable("SA_PASSWORD");

            if (!string.IsNullOrWhiteSpace(pw))
            {
                ServiceOptions = new MssqlDapperServiceOptions
                {
                    ConnectionString =
                        $"Data Source=mssql;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={pw};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                };

                DataService = new MssqlTestDataService(ServiceOptions, null);
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
                    var mssqlOptions = manager.ExtractSettings<MssqlDapperServiceOptions>();

                    ServiceOptions = new MssqlDapperServiceOptions
                    {
                        ConnectionString = mssqlOptions.ConnectionString
                    };
                    DataService = new MssqlTestDataService(ServiceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }
    }
}