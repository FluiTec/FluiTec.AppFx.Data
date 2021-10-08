using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.TestLibrary.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.UnitsOfWork
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
        /// Gets the date time dummy repository.
        /// </summary>
        ///
        /// <value>
        /// The date time dummy repository.
        /// </value>
        public IDateTimeDummyRepository DateTimeDummyRepository => GetRepository<IDateTimeDummyRepository>();

        /// <summary>   Registers the repositories.</summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository),
                (uow, log) => new NMemoryDummyRepository((NMemoryTestUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IDateTimeDummyRepository),
                (uow, log) => new NMemoryDateTimeDummyRepository((NMemoryTestUnitOfWork) uow, log));
        }
    }
}