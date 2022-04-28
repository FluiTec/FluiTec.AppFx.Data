using EfSample.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace EfSample.Data;

/// <summary>   Interface for test unit of work. </summary>
public interface ITestUnitOfWork : IUnitOfWork
{
    /// <summary>   Gets the dummy repository. </summary>
    /// <value> The dummy repository. </value>
    IDummyRepository DummyRepository { get; }
}