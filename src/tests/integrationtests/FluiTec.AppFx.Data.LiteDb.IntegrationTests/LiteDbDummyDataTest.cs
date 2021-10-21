using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a sqlite entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class LiteDbDummyDataTest : DummyDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LiteDbDummyDataTest() : base(new DbProvider())
        {
        }
    }
}
