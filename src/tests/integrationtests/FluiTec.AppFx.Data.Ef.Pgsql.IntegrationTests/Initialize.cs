using FluiTec.AppFx.Data.Dapper.Pgsql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Pgsql.IntegrationTests;

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

        PgsqlAdminHelper.CreateDababase(
            provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
            provider.AdminOptions.IntegrationDb);
        PgsqlAdminHelper.CreateUserAndLogin(
            provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
            provider.AdminOptions.IntegrationDb,
            provider.AdminOptions.IntegrationUser, provider.AdminOptions.IntegrationPassword);

        dataService.GetMigrator().Migrate();
    }
}