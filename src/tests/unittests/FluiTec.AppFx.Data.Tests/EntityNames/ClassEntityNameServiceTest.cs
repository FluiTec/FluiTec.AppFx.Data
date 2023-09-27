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
        var name1 = nameService.GetName(t);
        var name2 = nameService.GetName(t);

        Assert.IsNotNull(name1);
        Assert.AreEqual(expectedSchema, name1.Schema);
        Assert.AreEqual(expectedName, name1.Name);

        Assert.IsNotNull(name2);
        Assert.AreEqual(expectedSchema, name2.Schema);
        Assert.AreEqual(expectedName, name2.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnNull()
    {
        var nameService = new ClassEntityNameService();
        nameService.GetName(null!);
    }
}