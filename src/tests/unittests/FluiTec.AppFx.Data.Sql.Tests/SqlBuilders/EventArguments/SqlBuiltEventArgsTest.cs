using FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.EventArguments;

[TestClass]
public class SqlBuiltEventArgsTest
{
    [TestMethod]
    public void CanCreate()
    {
        var sql = "abc";
        var renderer = "xyz";

        var instance = new SqlBuiltEventArgs(sql, renderer);

        Assert.AreEqual(sql, instance.Sql);
        Assert.AreEqual(renderer, instance.Renderer);
    }
}