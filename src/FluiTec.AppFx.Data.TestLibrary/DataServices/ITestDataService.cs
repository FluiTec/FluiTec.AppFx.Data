using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.DataServices
{
    /// <summary>   Interface for test data service.</summary>
    public interface ITestDataService : IDataService<ITestUnitOfWork>
    {
    }
}