using Cli.Sample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace Cli.Sample.Data.Repositories;

public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
{
}