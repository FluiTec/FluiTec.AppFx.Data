using FluiTec.AppFx.Data.DataServices;

namespace EfSample.Data;

public interface ITestDataService : IDataService<ITestUnitOfWork>
{
}