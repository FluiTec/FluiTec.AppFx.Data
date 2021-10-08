using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a mssql date time dummy data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlDateTimeDummyDataTest : DateTimeDummyDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MssqlDateTimeDummyDataTest() : base(new DbProvider())
        {
        }
    }
}