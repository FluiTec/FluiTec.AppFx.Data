using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>   Interface for dummy repository.</summary>
    public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
    {
    }
}