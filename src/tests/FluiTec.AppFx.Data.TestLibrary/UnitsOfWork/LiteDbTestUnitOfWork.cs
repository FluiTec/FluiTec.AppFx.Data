using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.TestLibrary.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.UnitsOfWork
{
    /// <summary>
    ///     A lite database test unit of work.
    /// </summary>
    public class LiteDbTestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService,
            logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">      The data service. </param>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="logger">           The logger. </param>
        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork,
            ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        ///     Gets the dummy repository.
        /// </summary>
        /// <value>
        ///     The dummy repository.
        /// </value>
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
                (uow, log) => new LiteDbDummyRepository((LiteDbTestUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IDummy2Repository),
                (uow, log) => new LiteDbDummy2Repository((LiteDbTestUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(IDateTimeDummyRepository),
                (uow, log) => new LiteDbDateTimeDummyRepository((LiteDbTestUnitOfWork) uow, log));
        }
    }
}