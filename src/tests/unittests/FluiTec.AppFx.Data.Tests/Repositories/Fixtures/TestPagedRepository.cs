using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Tests.Repositories.Fixtures;

public class TestPagedRepository : PagedRepository<DummyEntity>
{
    public TestPagedRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork)
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

    public override Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<DummyEntity> GetPaged(int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public override IPagedResult<DummyEntity> GetPagedResult(int pageIndex, int pageSize)
    {
        throw new NotImplementedException();
    }

    public override Task<IEnumerable<DummyEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<IPagedResult<DummyEntity>> GetPagedResultAsync(int pageIndex, int pageSize,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}