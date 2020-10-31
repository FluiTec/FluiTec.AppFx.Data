using System;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    //[TestClass]
    //[TestCategory("Integration")]
    public class MssqlTest : DbTest
    {
        /// <summary>   Gets options for controlling the service.</summary>
        /// <value> Options that control the service.</value>
        protected sealed override IDapperServiceOptions ServiceOptions { get; }

        /// <summary>   Gets the data service.</summary>
        /// <value> The data service.</value>
        protected override ITestDataService DataService { get; }

        /// <summary>   Default constructor.</summary>
        public MssqlTest()
        {
            throw new NotImplementedException();
        }

        /// <summary>   (Unit Test Method) can check apply migrations.</summary>
        //[TestInitialize]
        public void CanCheckApplyMigrations()
        {
            AssertDbAvailable();

            var migrator = new DapperDataMigrator(ServiceOptions.ConnectionString, new[] { DataService.GetType().Assembly }, ((IDapperDataService)DataService).MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate();
        }
    }
}