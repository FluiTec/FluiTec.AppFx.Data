using System;

namespace FluiTec.AppFx.Data.Sql.EventArgs;

/// <summary>
/// Additional information for SQL generated events.
/// </summary>
public class SqlGeneratedEventArgs : SqlEventArgs
{
    /// <summary>
    /// Gets the type.
    /// </summary>
    ///
    /// <value>
    /// The type.
    /// </value>
    public Type Type { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="type">         The type. </param>
    /// <param name="statement">    The statement. </param>
    public SqlGeneratedEventArgs(Type type, string statement) : base(statement)
    {
        Type = type;
    }
}