using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) a lite database date time dummy data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class LiteDbDateTimeDummyDataTest : DateTimeDummyDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LiteDbDateTimeDummyDataTest() : base(new DbProvider())
        {
        }
    }
}