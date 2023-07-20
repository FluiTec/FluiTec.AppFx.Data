using System.Data;

namespace FluiTec.AppFx.Data.Dapper;

/// <summary>   Interface for connection factory. </summary>
public interface IConnectionFactory
{
    /// <summary>	Creates a connection. </summary>
    /// <param name="connectionString">	The connection string. </param>
    /// <returns>	The new connection. </returns>
    IDbConnection CreateConnection(string connectionString);
}