using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.NMemory
{
    /// <summary>
    /// A memory test unit of work.
    /// </summary>
    public class NMemoryTestUnitOfWork : NMemoryUnitOfWork, ITestUnitOfWork
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryTestUnitOfWork(INMemoryDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public NMemoryTestUnitOfWork(NMemoryUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        /// Gets the dummy repository.
        /// </summary>
        ///
        /// <value>
        /// The dummy repository.
        /// </value>
        public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();

        /// <summary>
        /// Gets the dummy 2 repository.
        /// </summary>
        ///
        /// <value>
        /// The dummy 2 repository.
        /// </value>
        public IDummy2Repository Dummy2Repository => GetRepository<IDummy2Repository>();

        /// <summary>   Registers the repositories.</summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository),
                (uow, log) => new NMemoryDummyRepository((NMemoryTestUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IDummy2Repository),
                (uow, log) => new NMemoryDummy2Repository((NMemoryTestUnitOfWork) uow, log));
        }
    }
}