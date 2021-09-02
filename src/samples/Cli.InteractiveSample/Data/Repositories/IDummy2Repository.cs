using Cli.InteractiveSample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace Cli.InteractiveSample.Data.Repositories
{
    public interface IDummy2Repository : IWritableKeyTableDataRepository<DummyEntity2, int>
    {
    }
}