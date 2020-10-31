using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql tests.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTest : DbTest
    {
        /// <summary>   Gets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected sealed override IDapperServiceOptions ServiceOptions { get; }

        /// <summary>   Gets the data service.</summary>
        /// <value> The data service.</value>
        protected override ITestDataService DataService { get; }

        /// <summary>   Default constructor.</summary>
        public PgsqlTest()
        {
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var usr = Environment.GetEnvironmentVariable("POSTGRES_USER");

            if (string.IsNullOrWhiteSpace(db) || string.IsNullOrWhiteSpace(usr)) return;

            ServiceOptions = new PgsqlDapperServiceOptions
            {
                ConnectionString = $"User ID={usr};Host=postgres;Database={db};Pooling=true;"
            };

            DataService = new PgsqlTestDataService(ServiceOptions, null);
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        [TestInitialize]
        public override void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new [] {DataService.GetType().Assembly}, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddPostgres());
            migrator.Migrate();
        }
    }
}