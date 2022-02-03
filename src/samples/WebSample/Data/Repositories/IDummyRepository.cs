using FluiTec.AppFx.Data.Repositories;
using WebSample.Data.Entities;

namespace WebSample.Data.Repositories;

public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
{
}