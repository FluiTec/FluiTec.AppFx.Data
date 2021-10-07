using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a pgsql entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlEntityDataTest : TestDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PgsqlEntityDataTest() : base(new DbProvider())
        {
        }
    }
}