using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;

public interface ITestUnitOfWork : IUnitOfWork<ITestDataService>
{
    /// <summary>   Gets the dummy repository. </summary>
    /// <value> The dummy repository. </value>
    public IRepository<DummyEntity> DummyRepository { get; }
}