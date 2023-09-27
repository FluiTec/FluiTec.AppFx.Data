using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;

/// <summary> Additional information for type SQL built events.</summary>
public class TypeSqlBuiltEventArgs : SqlBuiltEventArgs
{
    /// <summary> Constructor.</summary>
    /// <param name="sql">      The SQL. </param>
    /// <param name="renderer"> The renderer. </param>
    /// <param name="schema">   The schema. </param>
    public TypeSqlBuiltEventArgs(string sql, string renderer, ITypeSchema schema) : base(sql, renderer)
    {
        Schema = schema;
    }

    /// <summary> Gets the schema.</summary>
    /// <value> The schema.</value>
    public ITypeSchema Schema { get; }
}