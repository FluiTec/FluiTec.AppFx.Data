using FluiTec.AppFx.Data.LiteDb.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Repositories;

[TestClass]
public class LiteDbRepositoryTest : BaseLiteDbRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new LiteDbTestRepository(null!, MockProvider().Object, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new LiteDbTestRepository(MockService().Object, null!, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        new LiteDbTestRepository(MockService().Object, MockProvider().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(typeof(DummyEntity), repo.EntityType);
    }

    [TestMethod]
    public void SetsTableName()
    {
        var tableName = nameof(DummyEntity);

        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

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


        var repo = new LiteDbTestRepository(serviceMock.Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Logger);
    }

    [TestMethod]
    public void SetsTable()
    {
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Collection);
    }

    [TestMethod]
    public void CanGetAll()
    {
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        var res = repo.GetAll();
    }

    [TestMethod]
    public void CanGetAllAsync()
    {
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        var res = repo.GetAllAsync().Result;
    }

    [TestMethod]
    public void CanCount()
    {
        var count = 0;
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        Assert.AreEqual(count, repo.Count());
    }

    [TestMethod]
    public void CanCountAsync()
    {
        const int count = 0;
        var repo = new LiteDbTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        Assert.AreEqual(count, repo.CountAsync().Result);
    }
}