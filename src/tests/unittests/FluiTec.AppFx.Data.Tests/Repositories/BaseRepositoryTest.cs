using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.Repositories;

[TestClass]
public abstract class BaseRepositoryTest
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

    public virtual Mock<IDataProvider> MockProvider()
    {
        var providerMock = new Mock<IDataProvider>();
        providerMock
            .SetupGet(p => p.NameStrategy)
            .Returns(new DottedNameStrategy());
        return providerMock;
    }

    public virtual Mock<IUnitOfWork> MockUnitOfWork() => new Mock<IUnitOfWork>();
}