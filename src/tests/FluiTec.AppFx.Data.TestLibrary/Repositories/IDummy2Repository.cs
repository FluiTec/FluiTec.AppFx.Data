using System;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    /// Interface for dummy 2 repository.
    /// </summary>
    public interface IDummy2Repository : IWritableKeyTableDataRepository<Dummy2Entity, Guid>
    {
    }
}