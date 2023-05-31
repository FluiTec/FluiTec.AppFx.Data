using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Tests.EntityNames.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.EntityNames;

[TestClass]
public class ClassEntityNameServiceTest
{
    [TestMethod]
    [DataRow(null, nameof(TestEntity1), typeof(TestEntity1))]
    [DataRow(null, nameof(TestEntity2), typeof(TestEntity2))]
    [DataRow(null, nameof(TestEntity3), typeof(TestEntity3))]
    [DataRow(null, nameof(TestEntityWithSchema), typeof(TestEntityWithSchema))]
    public void CanGetByName(string expectedSchema, string expectedName, Type t)
    {
        var nameService = new ClassEntityNameService();
        var name = nameService.GetName(t);
        Assert.IsNotNull(name);
        Assert.AreEqual(expectedSchema, name.Schema);
        Assert.AreEqual(expectedName, name.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnNull()
    {
        var nameService = new ClassEntityNameService();
        nameService.GetName(null!);
    }
}