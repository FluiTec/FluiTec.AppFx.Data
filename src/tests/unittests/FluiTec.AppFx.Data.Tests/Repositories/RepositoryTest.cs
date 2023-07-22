using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable ObjectCreationAsStatement

namespace FluiTec.AppFx.Data.Tests.Repositories;

[TestClass]
public class RepositoryTest : BaseRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new TestRepository(null!, MockProvider().Object, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new TestRepository(MockService().Object, null!, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        new TestRepository(MockService().Object, MockProvider().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var repo = new TestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        Assert.AreEqual(typeof(DummyEntity), repo.EntityType);
    }

    [TestMethod]
    public void SetsDataService()
    {
        var serviceMock = MockService();

        var repo = new TestRepository(serviceMock.Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(serviceMock.Object, repo.DataService);
    }

    [TestMethod]
    public void SetsDataProvider()
    {
        var providerMock = MockProvider();
        
        var repo = new TestRepository(MockService().Object, providerMock.Object, MockUnitOfWork().Object);

        Assert.AreEqual(providerMock.Object, repo.DataProvider);
    }

    [TestMethod]
    public void SetsTableName()
    {
        var tableName = nameof(DummyEntity);

        var repo = new TestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var repo = new TestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNull(repo.Logger);
    }

    [TestMethod]
    public void SetsLogger()
    {
        var serviceMock = MockService();
        var logFactoryMock = new Mock<ILoggerFactory>();
        serviceMock
            .SetupGet(s => s.LoggerFactory)
            .Returns(logFactoryMock.Object);
        logFactoryMock
            .Setup(f => f.CreateLogger(It.IsAny<string>()))
            .Returns(new Mock<ILogger<Repository<DummyEntity>>>().Object);
        
        var repo = new TestRepository(serviceMock.Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Logger);
    }
}