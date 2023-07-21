using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Tests.Reflection.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Reflection;

[TestClass]
public class TypeSchemaTest
{
    [TestMethod]
    public void CanConstructUndecorated()
    {
        var schema = new TypeSchema(typeof(UndecoratedDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());
        Assert.IsNotNull(schema.Type);
        Assert.IsNotNull(schema);
    }

    [TestMethod]
    public void CanConstructDecorated()
    {
        var schema = new TypeSchema(typeof(DecoraredDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());
        Assert.IsNotNull(schema.Type);
        Assert.IsNotNull(schema);
    }

    [TestMethod]
    public void CanHanldeUndecoratedType()
    {
        var schema = new TypeSchema(typeof(UndecoratedDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(UndecoratedDummy.Id)));
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(UndecoratedDummy.Name)));

        Assert.IsTrue(schema.MappedProperties.Any(p => p.Name == nameof(UndecoratedDummy.Id)));
        Assert.IsTrue(schema.MappedProperties.Any(p => p.Name == nameof(UndecoratedDummy.Name)));

        Assert.IsTrue(!schema.KeyProperties.Any());
        Assert.IsTrue(!schema.UnmappedProperties.Any());
    }

    [TestMethod]
    public void CanHandleDecoratedType()
    {
        var schema = new TypeSchema(typeof(DecoraredDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(DecoraredDummy.Id1)));
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(DecoraredDummy.Id2)));
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(DecoraredDummy.Name)));
        Assert.IsTrue(schema.Properties.Any(p => p.Name == nameof(DecoraredDummy.Unmapped)));

        Assert.IsTrue(schema.MappedProperties.Any(p => p.Name == nameof(DecoraredDummy.Id1)));
        Assert.IsTrue(schema.MappedProperties.Any(p => p.Name == nameof(DecoraredDummy.Id2)));
        Assert.IsTrue(schema.MappedProperties.Any(p => p.Name == nameof(DecoraredDummy.Name)));

        Assert.IsTrue(schema.KeyProperties.Any(p => p.Name == nameof(DecoraredDummy.Id1)));
        Assert.IsTrue(schema.KeyProperties.Any(p => p.Name == nameof(DecoraredDummy.Id2)));

        Assert.IsTrue(schema.UnmappedProperties.Any(p => p.Name == nameof(DecoraredDummy.Unmapped)));
    }

    [TestMethod]
    public void CanGetValueByObject()
    {
        var obj = new DecoraredDummy { Id1 = 1, Id2 = 2 };
        var schema = new TypeSchema(typeof(DecoraredDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());
        var prop = schema.KeyProperties.Single(p => p.Name == nameof(DecoraredDummy.Id1));

        Assert.AreEqual(obj.Id1, prop.GetValue(obj));
    }
}