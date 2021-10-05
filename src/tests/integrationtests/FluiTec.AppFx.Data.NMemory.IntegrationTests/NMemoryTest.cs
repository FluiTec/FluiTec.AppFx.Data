using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests
{
    [TestClass]
    public class NMemoryTest : DbTest
    {
        public NMemoryTest(DataServiceProvider<ITestDataService, ITestUnitOfWork> provider) : base(provider)
        {
        }
    }
}