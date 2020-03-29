using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.DataServices
{
    [TestClass]
    public class MigratingDataServiceTest
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SetsCanMigrate(bool enabled)
        {
            var service = new TestDataService(enabled, "Test", null, null);
            Assert.AreEqual(enabled, service.CanMigrate);
        }

        protected class TestDataService : MigratingDataService
        {
            public TestDataService(bool canMigrate, string name, ILogger<IMigratingDataService> logger,
                ILoggerFactory loggerFactory) : base(
                canMigrate, name, logger, loggerFactory)
            {
            }

            public override void Dispose()
            {
                throw new NotImplementedException();
            }

            public override IUnitOfWork BeginUnitOfWork()
            {
                throw new NotImplementedException();
            }

            public override IUnitOfWork BeginUnitOfWork(IUnitOfWork other)
            {
                throw new NotImplementedException();
            }

            public override void Migrate()
            {
                throw new NotImplementedException();
            }
        }
    }
}