namespace FluiTec.AppFx.Data;

/// <summary>
/// Interface for SQL service options.
/// </summary>
public interface ISqlServiceOptions
{
    /// <summary>   Gets the connection string. </summary>
    /// <value> The connection string. </value>
    string ConnectionString { get; }
}