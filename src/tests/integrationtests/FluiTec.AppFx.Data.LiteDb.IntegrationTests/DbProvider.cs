using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.LiteDb.IntegrationTests
{
    /// <summary>
    /// A database provider.
    /// </summary>
    internal class DbProvider : LiteDbDataServiceProvider<ITestDataService, ITestUnitOfWork>
    {
        /// <summary>
        /// Provide data service.
        /// </summary>
        ///
        /// <returns>
        /// A TDataService.
        /// </returns>
        public override ITestDataService ProvideDataService() => new LiteDbTestDataService(ConfigureOptions(), null);

        /// <summary>
        /// Gets a value indicating whether the database is available.
        /// </summary>
        ///
        /// <value>
        /// True if the database is available, false if not.
        /// </value>
        public override bool IsDbAvailable => true;
    }
}