using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.IntegrationTests
{
    /// <summary>   (Unit Test Class) a pgsql tests.</summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTest : DbTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PgsqlTest() : base(new DbProvider())
        {
        }
    }
}