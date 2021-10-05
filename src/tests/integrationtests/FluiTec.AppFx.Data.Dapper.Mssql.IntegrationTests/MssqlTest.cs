using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mssql.IntegrationTests
{
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlTest : DbTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public MssqlTest() : base(new DbProvider())
        {
        }
    }
}