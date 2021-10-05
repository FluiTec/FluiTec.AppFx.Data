using FluiTec.AppFx.Data.TestLibrary;
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
        /// Default constructor.
        /// </summary>
        public SqliteTest() : base(new DbProvider())
        {
        }
    }
}
