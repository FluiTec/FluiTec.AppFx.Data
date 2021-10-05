using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
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
            var provider = new DbProvider();
            var dataService = provider.ProvideDataService();

            var migrator = new DapperDataMigrator(provider.ServiceOptions.ConnectionString,
                new[] {dataService.GetType().Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddSQLite());
            migrator.Migrate();
        }
    }
}