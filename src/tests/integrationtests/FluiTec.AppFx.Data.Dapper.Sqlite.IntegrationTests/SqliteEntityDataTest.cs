using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a sqlite entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class SqliteEntityDataTest : TestDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SqliteEntityDataTest() : base(new DbProvider())
        {
        }
    }
}
