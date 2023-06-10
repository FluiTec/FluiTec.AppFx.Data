using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.Tests.Schemata.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Schemata;

[TestClass]
public class MissingEntitySchemaExceptionTest
{
    [TestMethod]
    public void SetsEntityType()
    {
        var schema = new EmptyTestSchema();
        var type = typeof(DummyEntity);
        try
        {
            throw new MissingEntitySchemaException(schema, type);
        }
        catch (MissingEntitySchemaException e)
        {
            Assert.AreEqual(schema, e.Schema);
            Assert.AreEqual(type, e.EntityType);
        }
    }
}