using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   A postgres SQL statement builder. </summary>
public class PostgreSqlStatementBuilder : DefaultStatementBuilder
{
    /// <summary>   Default constructor. </summary>
    public PostgreSqlStatementBuilder() : base(new PostgreSqlBuilder())
    {
    }

    /// <summary>   Gets retrieve automatic insert key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The retrieve automatic insert key statement. </returns>
    public override string GetRetrieveAutoInsertKeyStatement(ITypeSchema typeSchema)
    {
        return $"RETURNING {SqlBuilder.RenderProperty(typeSchema.IdentityKey!)}";
    }
}