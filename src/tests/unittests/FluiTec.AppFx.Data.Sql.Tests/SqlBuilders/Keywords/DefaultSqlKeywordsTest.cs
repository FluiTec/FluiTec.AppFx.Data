using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.Keywords;

[TestClass]
public class DefaultSqlKeywordsTest
{
    protected virtual ISqlKeywords GetKeywords() => new DefaultSqlKeywords();

    [TestMethod]
    [DataRow("SELECT", nameof(ISqlKeywords.Select))]
    [DataRow("UPDATE", nameof(ISqlKeywords.Update))]
    [DataRow("DELETE", nameof(ISqlKeywords.Delete))]
    [DataRow("FROM", nameof(ISqlKeywords.From))]
    [DataRow("WHERE", nameof(ISqlKeywords.Where))]
    [DataRow("INSERT", nameof(ISqlKeywords.Insert))]
    [DataRow("INTO", nameof(ISqlKeywords.Into))]
    [DataRow("VALUES", nameof(ISqlKeywords.Values))]
    [DataRow("SET", nameof(ISqlKeywords.Set))]
    [DataRow("AND", nameof(ISqlKeywords.And))]
    [DataRow("OR", nameof(ISqlKeywords.Or))]
    [DataRow(",", nameof(ISqlKeywords.ListSeparator))]
    [DataRow(";", nameof(ISqlKeywords.CommandSeparator))]
    [DataRow("COUNT(*)", nameof(ISqlKeywords.CountAllExpression))]
    [DataRow("OFFSET", nameof(ISqlKeywords.OffsetExpression))]
    [DataRow("FETCH NEXT", nameof(ISqlKeywords.FetchNextExpressions))]
    [DataRow("ROWS", nameof(ISqlKeywords.OffsetRowsExpression))]
    [DataRow("ROWS ONLY", nameof(ISqlKeywords.FetchNextRowsExpression))]
    [DataRow("ORDER BY", nameof(ISqlKeywords.OrderByExpression))]
    [DataRow("ASC", nameof(ISqlKeywords.AscendingExpression))]
    [DataRow("DESC", nameof(ISqlKeywords.DescendingExpression))]
    [DataRow("=", nameof(ISqlKeywords.AssignEqualsOperator))]
    [DataRow("=", nameof(ISqlKeywords.CompareEqualsOperator))]

    public void CanMatch(string expected, string propertyName)
    {
        var obj = GetKeywords();
        var type = obj.GetType();
        var prop = type.GetProperty(propertyName);
        
        Assert.AreEqual(expected, prop!.GetValue(obj));
    }
}