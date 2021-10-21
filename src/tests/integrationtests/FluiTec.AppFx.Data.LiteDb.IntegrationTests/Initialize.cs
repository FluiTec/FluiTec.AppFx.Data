using System.IO;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var provider = new DbProvider() as LiteDbDataServiceProvider<ITestDataService, ITestUnitOfWork>;
            var options = provider.ConfigureOptions();
            if (File.Exists(options.FullDbFilePath))
                File.Delete(options.FullDbFilePath);
        }
    }
}