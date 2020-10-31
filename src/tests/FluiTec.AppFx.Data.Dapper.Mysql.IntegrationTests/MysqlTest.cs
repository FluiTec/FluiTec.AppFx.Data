using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mysql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a mysql test.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlTest : DbTest
    {
        /// <summary>   Initializes the options and data service.</summary>
        protected override void InitOptionsAndDataService()
        {
            var db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var pw = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");

            if (string.IsNullOrWhiteSpace(db) || string.IsNullOrWhiteSpace(pw)) return;

            ServiceOptions = new MysqlDapperServiceOptions
            {
                ConnectionString = $"Server=mysql;Database={db};Uid=root;Pwd={pw}"
            };

            DataService = new MysqlTestDataService(ServiceOptions, null);
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        [TestInitialize]
        public override void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new[] { DataService.GetType().Assembly }, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddMySql5());
            migrator.Migrate();
        }
    }
}