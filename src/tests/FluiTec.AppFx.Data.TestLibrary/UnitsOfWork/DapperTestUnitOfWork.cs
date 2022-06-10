using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.UnitsOfWork
{
    /// <summary>   A dapper test unit of work.</summary>
    public class DapperTestUnitOfWork : DapperUnitOfWork, ITestUnitOfWork
    {
        /// <summary>   Constructor.</summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public DapperTestUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService,
            logger)
        {
            RegisterRepositories();
        }

        /// <summary>   Constructor.</summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public DapperTestUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService,
            ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>   Gets the dummy repository.</summary>
        /// <value> The dummy repository.</value>
        public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();

        /// <summary> Gets the dummy 2 repository.</summary>
        ///
        /// <value> The dummy 2 repository.</value>
        public IDummy2Repository Dummy2Repository => GetRepository<IDummy2Repository>();

        /// <summary>
        ///     Gets the date time dummy repository.
        /// </summary>
        /// <value>
        ///     The date time dummy repository.
        /// </value>
        public IDateTimeDummyRepository DateTimeDummyRepository => GetRepository<IDateTimeDummyRepository>();

        /// <summary>   Registers the repositories.</summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository),
                (uow, log) => new DapperDummyRepository((DapperTestUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IDummy2Repository),
                (uow, log) => new DapperDummy2Repository((DapperTestUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IDateTimeDummyRepository),
                (uow, log) => new DapperDateTimeDummyRepository((DapperTestUnitOfWork) uow, log));
        }
    }
}