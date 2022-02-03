using FluiTec.AppFx.Data.DataServices;

namespace DynamicSample.Data;

public interface ITestDataService : IDataService<ITestUnitOfWork>
{
}