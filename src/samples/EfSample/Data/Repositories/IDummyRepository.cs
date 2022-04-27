using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace EfSample.Data.Repositories;

public interface IDummyRepository : IWritableTableDataRepository<DummyEntity>
{
}