using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDbSample.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace LiteDbSample.Data
{
    /// <summary>   A test unit of work. </summary>
    public class TestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="service">  The service. </param>
        /// <param name="logger">   The logger. </param>
        public TestUnitOfWork(ILiteDbDataService service, ILogger<IUnitOfWork> logger) : base(service, logger)
        {
            AddRepositories();
        }

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">      The data service. </param>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="logger">           The logger. </param>
        public TestUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork,
            ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
        {
            AddRepositories();
        }

        /// <summary>   Gets the dummy repository. </summary>
        /// <value> The dummy repository. </value>
        public DummyRepository DummyRepository => GetRepository<DummyRepository>();

        /// <summary>   Adds repositories. </summary>
        private void AddRepositories()
        {
            RepositoryProviders.Add(typeof(DummyRepository),
                (uow, log) => new DummyRepository((LiteDbUnitOfWork) uow, log));
        }
    }
}