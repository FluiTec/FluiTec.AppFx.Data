using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var serviceOptions = new SqliteDapperServiceOptions
            {
                ConnectionString = "Data Source=mydb.db;"
            };

            var dataService = new SqliteTestDataService(serviceOptions, null);

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] {dataService.GetType().Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddSQLite());
            migrator.Migrate();
        }
    }
}