using System.Transactions;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Schemata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Repositories;

[TestClass]
public abstract class BaseLiteDbRepositoryTest
{
    private class DumbSchema : Schema
    {
        public DumbSchema()
            : base(new AttributeEntityNameService(), new AttributePropertyNameService())
        {
        }

        public override ITypeSchema this[Type entityType] => new TypeSchema(entityType, EntityNameService, PropertyNameService);
    }

    public virtual Mock<IDataService> MockService()
    {
        var serviceMock = new Mock<IDataService>();
        serviceMock
            .SetupGet(s => s.Schema)
            .Returns(new DumbSchema());
        serviceMock
            .SetupGet(s => s.EntityNameService)
            .Returns(new AttributeEntityNameService());
        return serviceMock;
    }

    public virtual Mock<ILiteDbDataProvider> MockProvider()
    {
        const int count = 0;
        var items = Enumerable.Empty<DummyEntity>();
        var providerMock = new Mock<ILiteDbDataProvider>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(new DottedNameStrategy());
        providerMock
            .SetupGet(p => p.Database)
            .Returns(new LiteDatabase(":memory:"));
        return providerMock;
    }

    public virtual Mock<LiteDbUnitOfWork> MockUnitOfWork()
    {
        return new Mock<LiteDbUnitOfWork>(null, new TransactionOptions(), MockProvider().Object.Database);
    }
}