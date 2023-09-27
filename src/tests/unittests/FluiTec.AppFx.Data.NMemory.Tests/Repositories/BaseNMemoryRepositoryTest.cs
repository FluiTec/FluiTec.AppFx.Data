using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.Tests.Repositories;

[TestClass]
public abstract class BaseNMemoryRepositoryTest
{
    public virtual Mock<IDataService> MockService()
    {
        var serviceMock = new Mock<IDataService>();
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(new DumbSchema());
        return serviceMock;
    }

    public virtual Mock<INMemoryDataProvider> MockProvider()
    {
        const int count = 0;
        var items = Enumerable.Empty<DummyEntity>();
        var providerMock = new Mock<INMemoryDataProvider>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(new DottedNameStrategy());

        var tableMock = new Mock<ITable<DummyEntity>>();
        tableMock
            .Setup(m => m.Count)
            .Returns(() => count);
        tableMock.Setup(m => m.GetEnumerator()).Returns(() => items.GetEnumerator());
        providerMock
            .Setup(p => p.GetTable<DummyEntity>())
            .Returns(tableMock.Object);
        return providerMock;
    }

    public virtual Mock<NMemoryUnitOfWork> MockUnitOfWork()
    {
        return new Mock<NMemoryUnitOfWork>(null, new TransactionOptions());
    }

    private class DumbSchema : Schema
    {
        public DumbSchema()
            : base(new AttributeEntityNameService(), new AttributePropertyNameService())
        {
        }

        public override ITypeSchema this[Type entityType] =>
            new TypeSchema(entityType, EntityNameService, PropertyNameService);
    }
}