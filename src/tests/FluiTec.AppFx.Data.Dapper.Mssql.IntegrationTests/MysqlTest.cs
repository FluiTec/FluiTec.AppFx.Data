using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlTest : DbTest
    {
        /// <summary>   Gets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected sealed override IDapperServiceOptions ServiceOptions { get; }

        /// <summary>   Gets the data service.</summary>
        /// <value> The data service.</value>
        protected override ITestDataService DataService { get; }

        /// <summary>   Default constructor.</summary>
        public MysqlTest()
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
        public void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new[] { DataService.GetType().Assembly }, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddMySql5());
            migrator.Migrate();
        }
    }
}