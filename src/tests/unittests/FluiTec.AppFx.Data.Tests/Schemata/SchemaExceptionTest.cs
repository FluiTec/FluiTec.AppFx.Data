using FluiTec.AppFx.Data.Schemata;
using FluiTec.AppFx.Data.Tests.Schemata.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Schemata;

[TestClass]
public class SchemaExceptionTest
{
    [TestMethod]
    public void SetsSchema()
    {
        var schema = new EmptyTestSchema();
        try
        {
            throw new SchemaException(schema);
        }
        catch (SchemaException e)
        {
            Assert.AreEqual(schema, e.Schema);
        }
    }
}