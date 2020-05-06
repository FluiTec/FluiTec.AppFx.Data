using DynamicSample.Data.Entities;
using FluiTec.AppFx.Data.Repositories;

namespace DynamicSample.Data.Repositories
{
    public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
    {
        
    }
}