using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   A microsoft SQL statement builder. </summary>
public class MicrosoftSqlStatementBuilder : DefaultStatementBuilder
{
    /// <summary>   Default constructor. </summary>
    public MicrosoftSqlStatementBuilder() : base(new MicrosoftSqlBuilder())
    {
    }

    /// <summary>   Gets retrieve automatic insert key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The retrieve automatic insert key statement. </returns>
    public override string GetRetrieveAutoInsertKeyStatement(ITypeSchema typeSchema)
    {
        return $"{SqlBuilder.Keywords.Select} SCOPE_IDENTITY() {SqlBuilder.RenderProperty(typeSchema.IdentityKey!)}";
    }
}