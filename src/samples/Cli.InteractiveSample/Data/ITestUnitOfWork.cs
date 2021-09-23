using Cli.InteractiveSample.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace Cli.InteractiveSample.Data
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        /// <summary>   Gets the dummy repository. </summary>
        /// <value> The dummy repository. </value>
        // ReSharper disable once UnusedMember.Global
        IDummyRepository DummyRepository { get; }

        /// <summary>   Gets the dummy 2 repository. </summary>
        /// <value> The dummy 2 repository. </value>
        // ReSharper disable once UnusedMember.Global
        IDummy2Repository Dummy2Repository { get; }
    }
}