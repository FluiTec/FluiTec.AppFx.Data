using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a sqlite test.
    /// </summary>
    [TestClass]
    public class SqliteTest : DbTest
    {
        /// <summary>
        /// Initializes the options and data service.
        /// </summary>
        protected override void InitOptionsAndDataService()
        {
            ServiceOptions = new SqliteDapperServiceOptions
            {
                ConnectionString = "Data Source=mydb.db;"
            };

            DataService = new SqliteTestDataService(ServiceOptions, null);
        }
    }
}
