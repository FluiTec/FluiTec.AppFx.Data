using System;
using System.Data;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppRFx.Data.Dapper.Tests
{
    [TestClass]
    public class DapperUnitOfWorkTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsOnMissingDataService()
        {
            var unused = new DapperUnitOfWork(null, new Mock<ILogger<IUnitOfWork>>().Object);
        }

        [TestMethod]
        public void DoesNotThrowOnMissingLogger()
        {
            var transactionMock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            var serviceOptionsMock = new Mock<DapperServiceOptions>("test", connectionFactoryMock.Object);
            var dataServiceMock = new Mock<DapperDataService>(
                serviceOptionsMock.Object
                ,new Mock<ILogger<IDataService>>().Object
                ,new Mock<ILoggerFactory>().Object);

            connectionMock
                .Setup(connection => connection.BeginTransaction())
                .Returns(() => transactionMock.Object);

            connectionFactoryMock
                .Setup(factory => factory.CreateConnection("test"))
                .Returns(() => connectionMock.Object);

            var service = new DapperUnitOfWork(dataServiceMock.Object, null);
            Assert.IsNull(service.Logger);
        }

        [TestMethod]
        public void CanCommit()
        {
            var transactionMock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            var serviceOptionsMock = new Mock<DapperServiceOptions>("test", connectionFactoryMock.Object);
            var dataServiceMock = new Mock<DapperDataService>(
                serviceOptionsMock.Object
                ,new Mock<ILogger<IDataService>>().Object
                ,new Mock<ILoggerFactory>().Object);

            connectionMock
                .Setup(connection => connection.BeginTransaction())
                .Returns(() => transactionMock.Object);

            connectionFactoryMock
                .Setup(factory => factory.CreateConnection("test"))
                .Returns(() => connectionMock.Object);

            using var service = new DapperUnitOfWork(dataServiceMock.Object, null);
            service.Commit();
        }

        [TestMethod]
        public void CanRollback()
        {
            var transactionMock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            var serviceOptionsMock = new Mock<DapperServiceOptions>("test", connectionFactoryMock.Object);
            var dataServiceMock = new Mock<DapperDataService>(
                serviceOptionsMock.Object
                ,new Mock<ILogger<IDataService>>().Object
                ,new Mock<ILoggerFactory>().Object);

            connectionMock
                .Setup(connection => connection.BeginTransaction())
                .Returns(() => transactionMock.Object);

            connectionFactoryMock
                .Setup(factory => factory.CreateConnection("test"))
                .Returns(() => connectionMock.Object);

            using var service = new DapperUnitOfWork(dataServiceMock.Object, null);
            service.Rollback();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WontCommitDisposed()
        {
            var transactionMock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            var serviceOptionsMock = new Mock<DapperServiceOptions>("test", connectionFactoryMock.Object);
            var dataServiceMock = new Mock<DapperDataService>(
                serviceOptionsMock.Object
                ,new Mock<ILogger<IDataService>>().Object
                ,new Mock<ILoggerFactory>().Object);

            connectionMock
                .Setup(connection => connection.BeginTransaction())
                .Returns(() => transactionMock.Object);

            connectionFactoryMock
                .Setup(factory => factory.CreateConnection("test"))
                .Returns(() => connectionMock.Object);

            using var service = new DapperUnitOfWork(dataServiceMock.Object, null);
            service.Dispose();
            service.Commit();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WontRollbackDisposed()
        {
            var transactionMock = new Mock<IDbTransaction>();
            var connectionMock = new Mock<IDbConnection>();
            var connectionFactoryMock = new Mock<IConnectionFactory>();
            var serviceOptionsMock = new Mock<DapperServiceOptions>("test", connectionFactoryMock.Object);
            var dataServiceMock = new Mock<DapperDataService>(
                serviceOptionsMock.Object
                ,new Mock<ILogger<IDataService>>().Object
                ,new Mock<ILoggerFactory>().Object);

            connectionMock
                .Setup(connection => connection.BeginTransaction())
                .Returns(() => transactionMock.Object);

            connectionFactoryMock
                .Setup(factory => factory.CreateConnection("test"))
                .Returns(() => connectionMock.Object);

            using var service = new DapperUnitOfWork(dataServiceMock.Object, null);
            service.Dispose();
            service.Rollback();
        }
    }
}
