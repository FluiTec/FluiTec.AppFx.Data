using FluiTec.AppFx.Data.DataServices;

namespace Cli.InteractiveSample.Data;

public interface ITestDataService : IDataService<ITestUnitOfWork>
{
}