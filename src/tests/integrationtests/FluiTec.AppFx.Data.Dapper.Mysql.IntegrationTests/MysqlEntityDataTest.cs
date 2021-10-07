using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mysql.IntegrationTests
{
    /// <summary>
    /// A mysql entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlEntityDataTest : TestDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MysqlEntityDataTest() : base(new DbProvider())
        {
        }
    }
}