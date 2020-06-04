using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.Mssql
{
    public class MssqlTestUnitOfWork : DapperUnitOfWork, ITestUnitOfWork
    {
        public MssqlTestUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            RegisterRepositories();
        }

        public MssqlTestUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IDummyRepository), (uow, log) => new MssqlDummyRepository((MssqlTestUnitOfWork)uow, log));
        }

        public IDummyRepository DummyRepository => GetRepository<IDummyRepository>();
    }
}
