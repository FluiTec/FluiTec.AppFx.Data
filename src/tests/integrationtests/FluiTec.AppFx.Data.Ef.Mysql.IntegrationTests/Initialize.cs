using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Mysql.IntegrationTests;

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

        MysqlAdminHelper.CreateDababase(
            provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
            provider.AdminOptions.IntegrationDb);
        MysqlAdminHelper.CreateUserAndLogin(
            provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
            provider.AdminOptions.IntegrationDb,
            provider.AdminOptions.IntegrationUser, provider.AdminOptions.IntegrationPassword);

        System.Console.WriteLine($"LOG:Migrator.Migrate:{(dataService as EfTestDataService)!.ConnectionString}");

        dataService.GetMigrator().Migrate();
    }
}