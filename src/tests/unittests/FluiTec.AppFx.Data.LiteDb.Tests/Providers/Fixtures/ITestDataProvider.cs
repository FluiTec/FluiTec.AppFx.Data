using FluiTec.AppFx.Data.DataProviders;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers.Fixtures;

public interface ITestDataProvider : IDataProvider<ITestDataService, ITestUnitOfWork>
{
}