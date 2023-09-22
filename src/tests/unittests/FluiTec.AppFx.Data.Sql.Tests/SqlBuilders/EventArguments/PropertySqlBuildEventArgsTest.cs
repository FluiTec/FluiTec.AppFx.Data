using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.EventArguments;

[TestClass]
public class PropertySqlBuildEventArgsTest
{
    [TestMethod]
    public void CanCreate()
    {
        var sql = "abc";
        var renderer = "xyz";
        var schema = new PropertySchema(typeof(string), new PropertyName("Test", "Test"));

        var instance = new PropertySqlBuiltEventArgs(sql, renderer, schema);

        Assert.AreEqual(sql, instance.Sql);
        Assert.AreEqual(renderer, instance.Renderer);
        Assert.AreEqual(schema, instance.Schema);
    }
}