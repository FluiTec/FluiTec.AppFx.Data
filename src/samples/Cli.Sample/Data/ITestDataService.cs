using FluiTec.AppFx.Data.DataServices;

namespace Cli.Sample.Data;

public interface ITestDataService : IDataService<ITestUnitOfWork>
{
}