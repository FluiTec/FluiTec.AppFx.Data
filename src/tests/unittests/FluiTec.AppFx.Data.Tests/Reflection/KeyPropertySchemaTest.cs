using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Tests.Reflection.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Reflection;

[TestClass]
public class KeyPropertySchemaTest
{
    [TestMethod]
    public void CanGetValue()
    {
        var typeSchema = new TypeSchema(typeof(DecoraredDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());

        var entity = new DecoraredDummy { Id1 = 1, Id2 = 2, Name = "Test", Unmapped = "Test2" };

        Assert.AreEqual(typeSchema.KeyProperties.Single(kp => kp.Name == "Id1").GetValue(entity), entity.Id1);
        Assert.AreEqual(typeSchema.KeyProperties.Single(kp => kp.Name == "Id2").GetValue(entity), entity.Id2);
    }

    [TestMethod]
    public void CanSetValue()
    {
        var typeSchema = new TypeSchema(typeof(DecoraredDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());

        var entity = new DecoraredDummy { Id1 = 1, Id2 = 2, Name = "Test", Unmapped = "Test2" };

        typeSchema.KeyProperties.Single(kp => kp.Name == "Id1").SetValue(entity, 3);
        typeSchema.KeyProperties.Single(kp => kp.Name == "Id2").SetValue(entity, 4);

        Assert.AreEqual(3, entity.Id1);
        Assert.AreEqual(4, entity.Id2);
    }
}