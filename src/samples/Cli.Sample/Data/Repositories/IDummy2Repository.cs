using Cli.Sample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace Cli.Sample.Data.Repositories;

public interface IDummy2Repository : IWritableKeyTableDataRepository<DummyEntity2, int>
{
}