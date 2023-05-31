using FluiTec.AppFx.Data.EntityNames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.EntityNames;

[TestClass]
public class EntityNameTest
{
    [TestMethod]
    public void CanConstructOnlyName()
    {
        const string namestr = "Name";
        var name = new EntityName(null, namestr);
        Assert.IsNull(name.Schema);
        Assert.AreEqual(namestr, name.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnEmptyName()
    {
        const string namestr = "";
        var name = new EntityName(null, namestr);
        Assert.AreEqual(namestr, name.Name);
    }

    [TestMethod]
    public void CanConstructWithSchema()
    {
        const string schemastr = "Schema";
        const string namestr = "Name";
        var name = new EntityName(schemastr, namestr);
        Assert.AreEqual(schemastr, name.Schema);
        Assert.AreEqual(namestr, name.Name);
    }

    [TestMethod]
    public void MakesEmptySchemaNull()
    {
        const string namestr = "Name";
        var name = new EntityName(null, namestr);
        Assert.IsNull(name.Schema);
        Assert.AreEqual(namestr, name.Name);
    }
}