using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.DataServices
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsOnMissingName()
        {
            var unused = new TestDataService(null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsOnEmptyName()
        {
            var unused = new TestDataService(string.Empty, null, null);
        }

        [TestMethod]
        public void DoesNotThrowOnMissingLogger()
        {
            var service = new TestDataService("Test", null, null);
            Assert.IsNull(service.Logger);
        }

        [TestMethod]
        public void DoesNotThrowOnMissingLoggerFactory()
        {
            var service = new TestDataService("Test", null, null);
            Assert.IsNull(service.LoggerFactory);
        }

        [TestMethod]
        [DataRow("Test")]
        public void SetsName(string name)
        {
            var service = new TestDataService(name, null, null);
            Assert.AreEqual(name, service.Name);
        }

        protected class TestDataService : DataService
        {
            public TestDataService(string name, ILogger<IDataService> logger, ILoggerFactory loggerFactory) : base(name,
                logger, loggerFactory)
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
        }
    }
}