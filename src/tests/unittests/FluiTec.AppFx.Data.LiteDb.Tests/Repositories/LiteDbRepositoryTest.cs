using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
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
        new LiteDbTestRepository(null!, new Mock<ILiteDbDataProvider>().Object, new Mock<IUnitOfWork>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataProvider()
    {
        new LiteDbTestRepository(new Mock<IDataService>().Object, null!, new Mock<IUnitOfWork>().Object);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingUnitOfWork()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        nameServiceMock
            .Setup(n => n.GetName(It.IsAny<Type>()))
            .Returns(new EntityName(null, ""));
        new LiteDbTestRepository(serviceMock.Object, new Mock<ILiteDbDataProvider>().Object, null!);
    }

    [TestMethod]
    public void SetsEntityType()
    {
        var serviceMock = new Mock<IDataService>();
        var nameServiceMock = new Mock<IEntityNameService>();
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(nameServiceMock.Object);
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));

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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));

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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        var providerMock = new Mock<ILiteDbDataProvider>();
        var strategyMock = new Mock<INameStrategy>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(strategyMock.Object);
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));

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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));

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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));

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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));
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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));
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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));
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
        var schemaMock = new Mock<ISchema>();
        schemaMock
            .SetupGet(p => p[It.IsAny<Type>()])
            .Returns(new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
                new AttributePropertyNameService()));
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(schemaMock.Object);
        serviceMock
            .SetupGet(s => s.EntityNameService)
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

        var repo = new LiteDbTestRepository(serviceMock.Object, providerMock.Object, new LiteDbUnitOfWork(null, new TransactionOptions(), providerMock.Object.Database));
        Assert.AreEqual(count, repo.CountAsync().Result);
    }
}