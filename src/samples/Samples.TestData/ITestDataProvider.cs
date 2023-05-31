using FluiTec.AppFx.Data.DataProviders;

namespace Samples.TestData;

public interface ITestDataProvider : IDataProvider<ITestDataService, ITestUnitOfWork>
{
}