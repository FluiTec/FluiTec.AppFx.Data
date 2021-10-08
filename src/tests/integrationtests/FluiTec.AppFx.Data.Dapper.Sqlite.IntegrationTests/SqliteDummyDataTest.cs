using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a sqlite entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class SqliteDummyDataTest : DummyDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SqliteDummyDataTest() : base(new DbProvider())
        {
        }
    }
}
