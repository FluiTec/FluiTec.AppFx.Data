using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.Tests.Repositories;

[TestClass]
public class NMemoryRepositoryTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new NMemoryTestRepository(null!, new Mock<INMemoryDataProvider>().Object, new Mock<IUnitOfWork>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new NMemoryTestRepository(new Mock<IDataService>().Object, null!, new Mock<IUnitOfWork>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        new NMemoryTestRepository(new Mock<IDataService>().Object, new Mock<INMemoryDataProvider>().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);

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
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);

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
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);

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
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);

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
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(new Mock<ITable<DummyEntity>>().Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);

        Assert.IsNotNull(repo.Table);
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
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        var tableMock = new Mock<ITable<DummyEntity>>();
        tableMock
            .Setup(m => m.Count)
            .Returns(() => count);
        tableMock.Setup(m => m.GetEnumerator()).Returns(() => items.GetEnumerator());
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(tableMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);
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
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        var tableMock = new Mock<ITable<DummyEntity>>();
        tableMock
            .Setup(m => m.Count)
            .Returns(() => count);
        tableMock.Setup(m => m.GetEnumerator()).Returns(() => items.GetEnumerator());
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(tableMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);
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
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        var tableMock = new Mock<ITable<DummyEntity>>();
        tableMock
            .SetupGet(t => t.Count)
            .Returns(count);
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(tableMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);
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
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var providerMock = new Mock<INMemoryDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        var tableMock = new Mock<ITable<DummyEntity>>();
        tableMock
            .SetupGet(t => t.Count)
            .Returns(count);
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(tableMock.Object);
        strategyMock
            .Setup(s => s.ToString(It.IsAny<Type>(), It.IsAny<IEntityNameService>()))
            .Returns(tableName);

        var repo = new NMemoryTestRepository(serviceMock.Object, providerMock.Object, new Mock<IUnitOfWork>().Object);
        Assert.AreEqual(count, repo.CountAsync().Result);
    }
}