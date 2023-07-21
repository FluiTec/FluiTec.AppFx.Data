using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.NMemory.Tests.Repositories.Fixtures;

public class NMemoryTestRepository : NMemoryRepository<DummyEntity>
{
    public NMemoryTestRepository(IDataService dataService, INMemoryDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
    }
}