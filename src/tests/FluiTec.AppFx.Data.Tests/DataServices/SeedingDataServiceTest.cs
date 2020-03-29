using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.DataServices
{
    [TestClass]
    public class SeedingDataServiceTest
    {
        [TestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SetsCanSeed(bool enabled)
        {
            var service = new TestDataService(enabled, true, "Test", null, null);
            Assert.AreEqual(enabled, service.CanSeed);
        }

        protected class TestDataService : SeedingDataService
        {
            public TestDataService(bool canSeed, bool canMigrate, string name, ILogger<IMigratingDataService> logger,
                ILoggerFactory loggerFactory) :
                base(canSeed, canMigrate, name, logger, loggerFactory)
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

            public override void Seed()
            {
                throw new NotImplementedException();
            }
        }
    }
}