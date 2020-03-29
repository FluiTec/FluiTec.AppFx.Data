using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void CanGetRegisteredRepository()
        {
            var repo = new DummyUnitOfWork(new DummyDataService("Test", null, null)).GetRepository<IDummyRepository>();
            Assert.IsNotNull(repo);
        }

        public class DummyDataService : DataService
        {
            public DummyDataService(string name, ILogger<IDataService> logger, ILoggerFactory loggerFactory) : base(
                name, logger, loggerFactory)
            {
            }

            public override void Dispose()
            {
                throw new NotImplementedException();
            }

            public override IUnitOfWork BeginUnitOfWork()
            {
                return new DummyUnitOfWork(this);
            }

            public override IUnitOfWork BeginUnitOfWork(IUnitOfWork other)
            {
                throw new NotImplementedException();
            }
        }

        public class DummyUnitOfWork : UnitOfWork
        {
            public DummyUnitOfWork(IDataService dataService) : base(dataService, null)
            {
                RegisterRepositoryProvider(
                    new Func<IUnitOfWork, ILogger<IRepository>, IDummyRepository>((uow, logger) =>
                        new DummyRepository(logger)));
            }

            public override void Commit()
            {
                throw new NotImplementedException();
            }

            public override void Rollback()
            {
                throw new NotImplementedException();
            }

            protected override void Dispose(bool disposing)
            {
                throw new NotImplementedException();
            }
        }

        public class DummyEntity : IKeyEntity<int>
        {
            public int Id { get; set; }
        }

        public interface IDummyRepository : IWritableKeyTableDataRepository<DummyEntity, int>
        {
        }

        public class DummyRepository : IDummyRepository
        {

            public DummyRepository(ILogger<IRepository> logger)
            {
                Logger = logger;
            }

            public DummyEntity Get(int id)
            {
                throw new NotImplementedException();
            }

            public ILogger<IRepository> Logger { get; }

            public IEnumerable<DummyEntity> GetAll()
            {
                throw new NotImplementedException();
            }

            public int Count()
            {
                throw new NotImplementedException();
            }

            public string TableName => "Dummy";

            public DummyEntity Add(DummyEntity entity)
            {
                throw new NotImplementedException();
            }

            public void AddRange(IEnumerable<DummyEntity> entities)
            {
                throw new NotImplementedException();
            }

            public DummyEntity Update(DummyEntity entity)
            {
                throw new NotImplementedException();
            }

            public void Delete(int id)
            {
                throw new NotImplementedException();
            }

            public void Delete(DummyEntity entity)
            {
                throw new NotImplementedException();
            }
        }
    }
}