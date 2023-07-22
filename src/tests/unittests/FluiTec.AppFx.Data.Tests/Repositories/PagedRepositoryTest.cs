using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.Repositories;

[TestClass]
public class PagedRepositoryTest : BaseRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new TestPagedRepository(null!, MockProvider().Object, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new TestPagedRepository(MockService().Object, null!, MockUnitOfWork().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        new TestPagedRepository(MockService().Object, MockProvider().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var repo = new TestPagedRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(typeof(DummyEntity), repo.EntityType);
    }

    [TestMethod]
    public void SetsTableName()
    {
        var tableName = nameof(DummyEntity);

        var repo = new TestPagedRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var repo = new TestPagedRepository(MockService().Object, MockProvider().Object, MockUnitOfWork().Object);

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
       
        var repo = new TestPagedRepository(serviceMock.Object, MockProvider().Object, MockUnitOfWork().Object);

        Assert.IsNotNull(repo.Logger);
    }
}