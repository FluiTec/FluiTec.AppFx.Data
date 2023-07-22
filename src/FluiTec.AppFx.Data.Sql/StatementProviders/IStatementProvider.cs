using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.StatementProviders;

/// <summary> Interface for statement provider.</summary>
public interface IStatementProvider
{
    /// <summary>   Gets all statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   all statement. </returns>
    string GetAllStatement(ITypeSchema typeSchema);
}