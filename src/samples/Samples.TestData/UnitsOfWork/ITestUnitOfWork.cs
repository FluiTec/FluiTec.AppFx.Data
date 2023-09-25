using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Samples.TestData.DataServices;
using Samples.TestData.Entities;

namespace Samples.TestData.UnitsOfWork;

/// <summary>   Interface for test unit of work. </summary>
public interface ITestUnitOfWork : IUnitOfWork<ITestDataService>
{
    /// <summary>   Gets the dummy repository. </summary>
    /// <value> The dummy repository. </value>
    public IWritableTableDataRepository<DummyEntity> DummyRepository { get; }
}