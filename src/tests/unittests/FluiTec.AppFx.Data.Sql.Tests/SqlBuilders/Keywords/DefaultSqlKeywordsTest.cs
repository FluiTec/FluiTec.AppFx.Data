using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.Keywords;

[TestClass]
public class DefaultSqlKeywordsTest
{
    protected virtual ISqlKeywords GetKeywords() => new DefaultSqlKeywords();

    [TestMethod]
    [DataRow("SELECT", nameof(ISqlKeywords.Select))]
    [DataRow("FROM", nameof(ISqlKeywords.From))]
    [DataRow(",", nameof(ISqlKeywords.ListSeparator))]
    [DataRow("COUNT(*)", nameof(ISqlKeywords.CountAllExpression))]
    [DataRow("OFFSET", nameof(ISqlKeywords.OffsetExpression))]
    [DataRow("FETCH NEXT", nameof(ISqlKeywords.FetchNextExpressions))]
    [DataRow("ROWS", nameof(ISqlKeywords.OffsetRowsExpression))]
    [DataRow("ROWS ONLY", nameof(ISqlKeywords.FetchNextRowsExpression))]
    [DataRow("ORDER BY", nameof(ISqlKeywords.OrderByExpression))]
    [DataRow("ASC", nameof(ISqlKeywords.AscendingExpression))]
    [DataRow("DESC", nameof(ISqlKeywords.DescendingExpression))]
    public void CanMatch(string expected, string propertyName)
    {
        var obj = GetKeywords();
        var type = obj.GetType();
        var prop = type.GetProperty(propertyName);
        
        Assert.AreEqual(expected, prop!.GetValue(obj));
    }
}