using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a mssql entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlEntityDataTest : TestDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MssqlEntityDataTest() : base(new DbProvider())
        {
        }
    }
}