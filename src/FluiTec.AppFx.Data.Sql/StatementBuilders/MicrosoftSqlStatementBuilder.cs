using FluiTec.AppFx.Data.Sql.SqlBuilders;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   A microsoft SQL statement builder. </summary>
public class MicrosoftSqlStatementBuilder : DefaultStatementBuilder
{
    /// <summary>   Default constructor. </summary>
    public MicrosoftSqlStatementBuilder() : base(new MicrosoftSqlBuilder())
    {
    }
}