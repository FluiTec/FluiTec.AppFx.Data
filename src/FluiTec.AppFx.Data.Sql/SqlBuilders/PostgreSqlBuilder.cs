using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   A postgres SQL builder. </summary>
public class PostgreSqlBuilder : SqlBuilder
{
    /// <summary>   Default constructor. </summary>
    public PostgreSqlBuilder()
        : base(SqlType.Pgsql, new PostgreSqlKeywords())
    {
    }

    /// <summary>   Gets a value indicating whether the supports schemata. </summary>
    /// <value> True if supports schemata, false if not. </value>
    public override bool SupportsSchemata => true;

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public override string WrapExpression(string expression)
    {
        return $"\"{expression}\"";
    }
}