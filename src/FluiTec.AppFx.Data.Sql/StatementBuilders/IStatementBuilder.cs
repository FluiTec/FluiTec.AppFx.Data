using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   Interface for statement builder. </summary>
public interface IStatementBuilder
{
    /// <summary>   Gets the SQL builder. </summary>
    /// <value> The SQL builder. </value>
    ISqlBuilder SqlBuilder { get; }

    /// <summary>   Gets all statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   all statement. </returns>
    string GetAllStatement(ITypeSchema typeSchema);
}