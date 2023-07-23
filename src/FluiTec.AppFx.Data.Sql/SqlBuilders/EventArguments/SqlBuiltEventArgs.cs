using System;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;

/// <summary> Additional information for SQL built events.</summary>
public class SqlBuiltEventArgs : EventArgs
{
    /// <summary> Gets the SQL.</summary>
    /// <value> The SQL.</value>
    public string Sql { get; }

    /// <summary> Gets the renderer.</summary>
    /// <value> The renderer.</value>
    public string Renderer { get; }

    /// <summary> Constructor.</summary>
    /// <param name="sql">      The SQL. </param>
    /// <param name="renderer"> The renderer. </param>
    public SqlBuiltEventArgs(string sql, string renderer)
    {
        Sql = sql;
        Renderer = renderer;
    }
}