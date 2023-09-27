using System.Collections;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.Tests.Schemata.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Schemata;

[TestClass]
public class SchemaTest
{
    [TestMethod]
    public void CanConstructEmpty()
    {
        var schema = new EmptyTestSchema();
        Assert.IsNotNull(schema);
    }

    [TestMethod]
    public void CanConstructDummy()
    {
        var schema = new DummyTestSchema();
        Assert.IsNotNull(schema);
    }

    [TestMethod]
    public void CanEnumerateEmptyIEnumerable()
    {
        var schema = new EmptyTestSchema();
        foreach (var item in schema as IEnumerable)
            Assert.IsNotNull(item);
    }

    [TestMethod]
    public void CanEnumerateEmpty()
    {
        var schema = new EmptyTestSchema();
        foreach (var item in schema)
            Assert.IsNotNull(item);
    }

    [TestMethod]
    public void CanEnumerateDummyIEnumerable()
    {
        var schema = new DummyTestSchema();
        foreach (var item in schema as IEnumerable)
            Assert.IsNotNull(item);
    }

    [TestMethod]
    public void CanEnumerateDummy()
    {
        var schema = new DummyTestSchema();
        foreach (var item in schema)
            Assert.IsNotNull(item);
    }

    [TestMethod]
    public void CanIndex()
    {
        var typeSchema = new DummyTestSchema();
        Assert.IsNotNull(typeSchema[typeof(DummyEntity)]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnNullIndexer()
    {
        var typeSchema = new EmptyTestSchema()[null!];
        Assert.IsNotNull(typeSchema);
    }

    [TestMethod]
    [ExpectedException(typeof(MissingEntitySchemaException))]
    public void ThrowsOnMissingSchemaIndexer()
    {
        var typeSchema = new EmptyTestSchema()[typeof(UnregisteredDummyEntity)];
        Assert.IsNotNull(typeSchema);
    }

    [TestMethod]
    public void CanFindIdentityKey()
    {
        var typeSchema = new IdentityDummyTestSchema()[typeof(IdentityDummyEntity)];
        Assert.IsNotNull(typeSchema.IdentityKey);
        Assert.IsTrue(typeSchema.UsesIdentityKey);
    }

    [TestMethod]
    [ExpectedException(typeof(NonSingularIdentityKeyException))]
    public void ThrowsOnNonSingularIdentityKey()
    {
        var typeSchema = new NonSingularIdentityDummyTestSchema()[typeof(NonSingularIdentityDummyEntity)];
    }
}