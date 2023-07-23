using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;

/// <summary> Additional information for property SQL built events.</summary>
public class PropertySqlBuiltEventArgs : SqlBuiltEventArgs
{
    /// <summary> Gets the schema.</summary>
    /// <value> The schema.</value>
    public IPropertySchema Schema { get; }

    /// <summary> Constructor.</summary>
    /// <param name="sql">      The SQL. </param>
    /// <param name="renderer"> The renderer. </param>
    /// <param name="schema">   The schema. </param>
    public PropertySqlBuiltEventArgs(string sql, string renderer, IPropertySchema schema) : base(sql, renderer)
    {
        Schema = schema;
    }
}