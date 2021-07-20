using FluiTec.AppFx.Data.Repositories;
using WebSample.Data.Entities;

namespace WebSample.Data.Repositories
{
    public interface IDummy2Repository : IWritableKeyTableDataRepository<DummyEntity2, int>
    {
    }
}