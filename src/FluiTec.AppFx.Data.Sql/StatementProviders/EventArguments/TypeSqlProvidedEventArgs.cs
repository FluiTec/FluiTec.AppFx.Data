using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.StatementProviders.EventArguments;

/// <summary> Additional information for type SQL provided events.</summary>
public class TypeSqlProvidedEventArgs : SqlProvidedEventArgs
{
    /// <summary> Constructor.</summary>
    /// <param name="sql">      The SQL. </param>
    /// <param name="provider"> The provider. </param>
    /// <param name="schema">   The schema. </param>
    public TypeSqlProvidedEventArgs(string sql, string provider, ITypeSchema schema) : base(sql, provider)
    {
        Schema = schema;
    }

    /// <summary> Gets the schema.</summary>
    /// <value> The schema.</value>
    public ITypeSchema Schema { get; }
}