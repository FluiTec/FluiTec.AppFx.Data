using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace DynamicSample.Data
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        IDummyRepository DummyRepository { get; }
    }
}