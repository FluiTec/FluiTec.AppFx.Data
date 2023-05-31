using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.EntityNames;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluiTec.AppFx.Data.Tests.EntityNames.Fixtures;
using Moq;

namespace FluiTec.AppFx.Data.Tests.EntityNames.NameStrategies;

[TestClass]
public class DottedNameStrategyTest
{
    [TestMethod]
    [DataRow("Schema.Name", "Schema", "Name")]
    [DataRow("Name", "", "Name")]
    [DataRow("Name", null, "Name")]
    public void CanGenerateByName(string expectedOutput, string schema, string name)
    {
        var strategy = new DottedNameStrategy();
        var entityName = new EntityName(schema, name);
        Assert.AreEqual(expectedOutput, strategy.ToString(entityName));
    }

    [TestMethod]
    [DataRow("TestEntity1", typeof(TestEntity1), false)]
    [DataRow("TestEntity2", typeof(TestEntity2), false)]
    [DataRow("TestEntity3", typeof(TestEntity3), false)]
    [DataRow("Schema.TestEntity1", typeof(TestEntity1), true)]
    [DataRow("Schema.TestEntity2", typeof(TestEntity2), true)]
    [DataRow("Schema.TestEntity3", typeof(TestEntity3), true)]
    public void CanGenerateByTypeClassName(string expectedOutput, Type type, bool withSchema)
    {
        const string schema = "Schema";
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(withSchema ? schema : null, type.Name));

        var strategy = new DottedNameStrategy();
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }

    [TestMethod]
    [DataRow("Schema.TestEntity1", typeof(TestEntity1))]
    [DataRow("Schema.TestEntity2", typeof(TestEntity2))]
    [DataRow("Schema.TestEntity3", typeof(TestEntity3))]
    public void CanGenerateByTypeClassNameWithSchema(string expectedOutput, Type type)
    {
        const string schema = "Schema";
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(schema, type.Name));

        var strategy = new DottedNameStrategy();
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }
}