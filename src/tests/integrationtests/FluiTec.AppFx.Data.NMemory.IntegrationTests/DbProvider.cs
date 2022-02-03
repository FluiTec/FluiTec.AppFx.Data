using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests;

/// <summary>
///     A database provider.
/// </summary>
internal class DbProvider : DataServiceProvider<ITestDataService, ITestUnitOfWork>
{
    /// <summary>
    ///     Gets a value indicating whether the database is available.
    /// </summary>
    /// <value>
    ///     True if the database is available, false if not.
    /// </value>
    public override bool IsDbAvailable => true;

    /// <summary>
    ///     Provide data service.
    /// </summary>
    /// <returns>
    ///     A TDataService.
    /// </returns>
    public override ITestDataService ProvideDataService()
    {
        return new NMemoryTestDataService(null);
    }
}