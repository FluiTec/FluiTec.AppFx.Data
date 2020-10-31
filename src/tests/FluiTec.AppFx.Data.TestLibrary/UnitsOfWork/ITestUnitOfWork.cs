using FluiTec.AppFx.Data.TestLibrary.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.UnitsOfWork
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        /// <summary>   Gets the dummy repository. </summary>
        /// <value> The dummy repository. </value>
        IDummyRepository DummyRepository { get; }
    }
}