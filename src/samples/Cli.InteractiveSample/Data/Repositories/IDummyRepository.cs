using Cli.InteractiveSample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace Cli.InteractiveSample.Data.Repositories
{
    public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
    {
    }
}