using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Repositories.Fixtures;

public class LiteDbTestRepository : LiteDbRepository<DummyEntity>
{
    public LiteDbTestRepository(IDataService dataService, ILiteDbDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
    }
}