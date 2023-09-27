using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;
using FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.EventArguments;

[TestClass]
public class TypeSqlBuildEventArgsTest
{
    [TestMethod]
    public void CanCreate()
    {
        var sql = "abc";
        var renderer = "xyz";
        var schema = new TypeSchema(typeof(DecoratedIdentityDummy), new AttributeEntityNameService(),
            new AttributePropertyNameService());

        var instance = new TypeSqlBuiltEventArgs(sql, renderer, schema);

        Assert.AreEqual(sql, instance.Sql);
        Assert.AreEqual(renderer, instance.Renderer);
        Assert.AreEqual(schema, instance.Schema);
    }
}