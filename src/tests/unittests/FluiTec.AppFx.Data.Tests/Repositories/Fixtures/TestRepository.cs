using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Tests.Repositories.Fixtures;

public class TestRepository : Repository<DummyEntity>
{
    public TestRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
    }

    public override IEnumerable<DummyEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<DummyEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override long Count()
    {
        throw new NotImplementedException();
    }

    public override Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}