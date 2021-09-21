using Cli.Sample.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace Cli.Sample.Data
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        /// <summary>   Gets the dummy repository. </summary>
        /// <value> The dummy repository. </value>
        IDummyRepository DummyRepository { get; }

        /// <summary>   Gets the dummy 2 repository. </summary>
        /// <value> The dummy 2 repository. </value>
        IDummy2Repository Dummy2Repository { get; }
    }
}