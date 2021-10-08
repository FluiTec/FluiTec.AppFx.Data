using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    /// Interface for date time dummy repository.
    /// </summary>
    public interface IDateTimeDummyRepository : IWritableKeyTableDataRepository<DateTimeDummyEntity, int>
    {
    }
}