using FluiTec.AppFx.Data.NMemory.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.NMemory.Tests.Repositories;

[TestClass]
public class NMemoryRepositoryTest : BaseNMemoryRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new NMemoryTestRepository(null!, MockProvider().Object, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new NMemoryTestRepository(MockService().Object, null!, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        new NMemoryTestRepository(MockService().Object, MockProvider().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(typeof(DummyEntity), repo.EntityType);
    }

    [TestMethod]
    public void SetsTableName()
    {
        var tableName = nameof(DummyEntity);

        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

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

        var repo = new NMemoryTestRepository(serviceMock.Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Logger);
    }

    [TestMethod]
    public void SetsTable()
    {
        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Table);
    }

    [TestMethod]
    public void CanGetAll()
    {
        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        var res = repo.GetAll();
    }

    [TestMethod]
    public void CanGetAllAsync()
    {
        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);
        var res = repo.GetAllAsync().Result;
    }

    [TestMethod]
    public void CanCount()
    {
        const int count = 0;

        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(count, repo.Count());
    }

    [TestMethod]
    public void CanCountAsync()
    {
        const int count = 0;

        var repo = new NMemoryTestRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(count, repo.CountAsync().Result);
    }
}