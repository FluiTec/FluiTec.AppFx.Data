using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
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
            var provider = new DbProvider();
            var dataService = provider.ProvideDataService();
            
            MysqlAdminHelper.CreateDababase(provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString, provider.AdminOptions.IntegrationDb);
            MysqlAdminHelper.CreateUserAndLogin(provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString, provider.AdminOptions.IntegrationDb,
                provider.AdminOptions.IntegrationUser, provider.AdminOptions.IntegrationPassword);

            var migrator = new DapperDataMigrator(provider.ServiceOptions.ConnectionString,
                new[] {dataService.GetType().BaseType?.Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddMySql5());
            migrator.Migrate();
        }
    }
}