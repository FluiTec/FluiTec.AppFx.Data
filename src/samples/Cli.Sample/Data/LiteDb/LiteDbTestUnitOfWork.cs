using Cli.Sample.Data.Repositories;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace Cli.Sample.Data.LiteDb
{
    public class LiteDbTestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
    {
        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService,
            logger)
        {
            RegisterRepositories();
        }

        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork,
            ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
        {
            RegisterRepositories();
        }

        public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();
        public IDummy2Repository Dummy2Repository => GetRepository<IDummy2Repository>();

        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository),
                (uow, log) => new LiteDbDummyRepository((LiteDbUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IDummy2Repository),
                (uow, log) => new LiteDbDummy2Repository((LiteDbUnitOfWork) uow, log));
        }
    }
}