using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Tests.EntityNames.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.EntityNames.NameStrategies;

[TestClass]
public class UnderscoredNameStrategyTest
{
    [TestMethod]
    [DataRow("Schema_Name", "Schema", "Name")]
    [DataRow("Name", "", "Name")]
    [DataRow("Name", null, "Name")]
    public void CanGenerateByName(string expectedOutput, string schema, string name)
    {
        var strategy = new UnderscoredNameStrategy();
        var entityName = new EntityName(schema, name);
        Assert.AreEqual(expectedOutput, strategy.ToString(entityName));
    }

    [TestMethod]
    [DataRow("TestEntity1", typeof(TestEntity1), false)]
    [DataRow("TestEntity2", typeof(TestEntity2), false)]
    [DataRow("TestEntity3", typeof(TestEntity3), false)]
    [DataRow("Schema_TestEntity1", typeof(TestEntity1), true)]
    [DataRow("Schema_TestEntity2", typeof(TestEntity2), true)]
    [DataRow("Schema_TestEntity3", typeof(TestEntity3), true)]
    public void CanGenerateByTypeClassName(string expectedOutput, Type type, bool withSchema)
    {
        const string schema = "Schema";
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(withSchema ? schema : null, type.Name));

        var strategy = new UnderscoredNameStrategy();
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }

    [TestMethod]
    [DataRow("Schema_TestEntity1", typeof(TestEntity1))]
    [DataRow("Schema_TestEntity2", typeof(TestEntity2))]
    [DataRow("Schema_TestEntity3", typeof(TestEntity3))]
    public void CanGenerateByTypeClassNameWithSchema(string expectedOutput, Type type)
    {
        const string schema = "Schema";
        var mockNameService = new Mock<IEntityNameService>();
        mockNameService
            .Setup(m => m.GetName(It.IsAny<Type>()))
            .Returns(() => new EntityName(schema, type.Name));

        var strategy = new UnderscoredNameStrategy();
        Assert.AreEqual(expectedOutput, strategy.ToString(type, mockNameService.Object));
    }
}