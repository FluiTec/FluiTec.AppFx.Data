using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders.Keywords;

[TestClass]
public class PostgreSqlKeywordsTest : DefaultSqlKeywordsTest
{
    protected override ISqlKeywords GetKeywords() => new PostgreSqlKeywords();
}