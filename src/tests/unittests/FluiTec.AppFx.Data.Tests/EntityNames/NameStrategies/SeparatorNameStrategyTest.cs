using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Tests.EntityNames.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.EntityNames.NameStrategies;

[TestClass]
public class SeparatorNameStrategyTest
{
    [TestMethod]
    [DataRow("Schema.Name", ".", "Schema", "Name")]
    [DataRow("Schema_Name", "_", "Schema", "Name")]
    [DataRow("Schema-Name", "-", "Schema", "Name")]
    [DataRow("Name", ".", "", "Name")]
    [DataRow("Name", "_", "", "Name")]
    [DataRow("Name", "-", "", "Name")]
    [DataRow("Name", ".", null, "Name")]
    [DataRow("Name", "_", null, "Name")]
    [DataRow("Name", "-", null, "Name")]
    public void CanGenerateByName(string expectedOutput, string separator, string schema, string name)
    {
        var strategy = new SeparatorNameStrategy(separator);
        var entityName = new EntityName(schema, name);
        Assert.AreEqual(expectedOutput, strategy.ToString(entityName));
    }

    [TestMethod]
    [DataRow("TestEntity1", typeof(TestEntity1))]
    [DataRow("TestEntity2", typeof(TestEntity2))]
    [DataRow("TestEntity3", typeof(TestEntity3))]
    public void CanGenerateByTypeClassName(string expectedOutput, Type type)
    {
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(null, type.Name));

        var strategy = new SeparatorNameStrategy(string.Empty);
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }

    [TestMethod]
    [DataRow("Schema.TestEntity1", ".", typeof(TestEntity1))]
    [DataRow("Schema.TestEntity2", ".", typeof(TestEntity2))]
    [DataRow("Schema.TestEntity3", ".", typeof(TestEntity3))]
    [DataRow("Schema_TestEntity1", "_", typeof(TestEntity1))]
    [DataRow("Schema_TestEntity2", "_", typeof(TestEntity2))]
    [DataRow("Schema_TestEntity3", "_", typeof(TestEntity3))]
    [DataRow("Schema-TestEntity1", "-", typeof(TestEntity1))]
    [DataRow("Schema-TestEntity2", "-", typeof(TestEntity2))]
    [DataRow("Schema-TestEntity3", "-", typeof(TestEntity3))]
    public void CanGenerateByTypeClassNameWithSchema(string expectedOutput, string separator, Type type)
    {
        const string schema = "Schema";
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(schema, type.Name));

        var strategy = new SeparatorNameStrategy(separator);
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }
}