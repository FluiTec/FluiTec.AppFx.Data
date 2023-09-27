using System;

namespace FluiTec.AppFx.Data.Sql.StatementProviders.EventArguments;

/// <summary> Additional information for SQL provided events.</summary>
public class SqlProvidedEventArgs : EventArgs
{
    /// <summary> Constructor.</summary>
    /// <param name="sql">      The SQL. </param>
    /// <param name="provider"> The provider. </param>
    public SqlProvidedEventArgs(string sql, string provider)
    {
        Sql = sql;
        Provider = provider;
    }

    /// <summary> Gets the SQL.</summary>
    /// <value> The SQL.</value>
    public string Sql { get; }

    /// <summary> Gets the provider.</summary>
    /// <value> The provider.</value>
    public string Provider { get; }
}