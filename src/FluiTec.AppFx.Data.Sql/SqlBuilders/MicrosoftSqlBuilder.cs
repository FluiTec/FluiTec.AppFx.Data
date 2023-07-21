using FluiTec.AppFx.Data.Sql.Enums;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   A microsoft SQL builder. </summary>
public class MicrosoftSqlBuilder : SqlBuilder
{
    /// <summary>   Constructor. </summary>
    public MicrosoftSqlBuilder() 
        : base(SqlType.Mssql, new MicrosoftSqlKeywords())
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
        return $"[{expression}]";
    }
}