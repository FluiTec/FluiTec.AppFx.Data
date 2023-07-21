using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.Repositories;

[TestClass]
public class PagedRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new TestPagedRepository(null!, new Mock<IDataProvider>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new TestPagedRepository(new Mock<IDataService>().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<IDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new TestPagedRepository(serviceMock.Object, providerMock.Object);

        Assert.AreEqual(typeof(DummyEntity), repo.EntityType);
    }

    [TestMethod]
    public void SetsTableName()
    {
        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<IDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new TestPagedRepository(serviceMock.Object, providerMock.Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<IDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new TestPagedRepository(serviceMock.Object, providerMock.Object);

        Assert.IsNull(repo.Logger);
    }

    [TestMethod]
    public void SetsLogger()
    {
        var serviceMock = new Mock<IDataService>();
        var logFactoryMock = new Mock<ILoggerFactory>();
        serviceMock
            .SetupGet(s => s.LoggerFactory)
            .Returns(logFactoryMock.Object);
        logFactoryMock
            .Setup(f => f.CreateLogger(It.IsAny<string>()))
            .Returns(new Mock<ILogger<Repository<DummyEntity>>>().Object);
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<IDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new TestPagedRepository(serviceMock.Object, providerMock.Object);

        Assert.IsNotNull(repo.Logger);
    }
}