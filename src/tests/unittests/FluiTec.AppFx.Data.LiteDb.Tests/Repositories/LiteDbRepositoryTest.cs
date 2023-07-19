using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Repositories;

[TestClass]
public class LiteDbRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new LiteDbTestRepository(null!, new Mock<ILiteDbDataProvider>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new LiteDbTestRepository(new Mock<IDataService>().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);

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
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);

        Assert.AreEqual(tableName, repo.TableName);
    }

    [TestMethod]
    public void SetsNullLogger()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);

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
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);

        Assert.IsNotNull(repo.Logger);
    }

    [TestMethod]
    public void SetsTable()
    {
        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);

        Assert.IsNotNull(repo.Collection);
    }

    [TestMethod]
    public void CanGetAll()
    {
        const int count = 0;
        var items = Enumerable.Empty<DummyEntity>();

        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);
        var res = repo.GetAll();
    }

    [TestMethod]
    public void CanGetAllAsync()
    {
        const int count = 0;
        var items = Enumerable.Empty<DummyEntity>();

        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);
        var res = repo.GetAllAsync().Result;
    }

    [TestMethod]
    public void CanCount()
    {
        const int count = 0;
        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);
        Assert.AreEqual(count, repo.Count());
    }

    [TestMethod]
    public void CanCountAsync()
    {
        const int count = 0;
        var tableName = nameof(DummyEntity);
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, tableName));
        serviceMock
            .SetupGet(s => s.NameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object);
        Assert.AreEqual(count, repo.CountAsync().Result);
    }
}