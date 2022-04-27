namespace FluiTec.AppFx.Data.Dapper;

/// <summary>   Interface for dapper service options. </summary>
public interface IDapperServiceOptions : ISqlServiceOptions
{
    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    IConnectionFactory ConnectionFactory { get; }
}