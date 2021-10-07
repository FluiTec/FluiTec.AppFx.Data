using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests
{
    /// <summary>
    /// A memory entity data test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class NMemoryEntityDataTest : TestDataTest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NMemoryEntityDataTest() : base(new DbProvider())
        {

        }
    }
}