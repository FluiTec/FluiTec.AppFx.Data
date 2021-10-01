using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests
{
    [TestClass]
    public class NMemoryTest : DbTest
    {
        /// <summary>
        /// Initializes the options and data service.
        /// </summary>
        protected override void InitOptionsAndDataService()
        {
            DataService = new NMemoryTestDataService(null);
        }

        protected override void AssertDbAvailable()
        {
            // do nothing here - we've got no options
        }
    }
}
