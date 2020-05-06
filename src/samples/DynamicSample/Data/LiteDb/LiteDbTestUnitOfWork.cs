using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.LiteDb
{
    public class LiteDbTestUnitOfWork : LiteDbUnitOfWork, ITestUnitOfWork
    {
        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
        }

        public LiteDbTestUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork, ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
        {
        }

        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository), (uow, log) => new LiteDbDummyRepository((LiteDbUnitOfWork)uow, log));
        }

        public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();
    }
}
