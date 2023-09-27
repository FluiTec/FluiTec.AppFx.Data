using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Tests.Reflection.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Reflection;

[TestClass]
public class NonSingularIdentityKeyExceptionTest
{
    [TestMethod]
    public void CanGetType()
    {
        var type = typeof(DecoraredDummy);
        var ex = new NonSingularIdentityKeyException(type);
        Assert.AreEqual(type, ex.Type);
    }
}