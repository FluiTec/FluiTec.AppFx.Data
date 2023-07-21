using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Reflection;

[TestClass]
public class PropertySchemaTest
{
    [TestMethod]
    public void CanCompareEqual()
    {
        var o1 = new PropertySchema(typeof(int), new PropertyName("Id", "Id"));
        var o2 = new PropertySchema(typeof(int), new PropertyName("Id", "Id"));

        Assert.IsTrue(o1.Equals(o2));
        Assert.IsTrue(o1.Equals((object)o2));
    }

    [TestMethod]
    public void CanCompareRefEqual()
    {
        var o1 = new PropertySchema(typeof(int), new PropertyName("Id", "Id"));

        Assert.IsTrue(o1.Equals(o1));
        Assert.IsTrue(o1.Equals((object)o1));
    }

    [TestMethod]
    public void CanCompareNull()
    {
        var o1 = new PropertySchema(typeof(int), new PropertyName("Id", "Id"));
        object e = null!;
        Assert.IsFalse(o1.Equals(e));
    }
}