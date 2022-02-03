using System.Data;
using Microsoft.Data.Sqlite;

namespace FluiTec.AppFx.Data.Dapper.SqLite;

/// <summary>   A sqlite connection factory. </summary>
public class SqliteConnectionFactory : IConnectionFactory
{
    /// <summary>   Creates a connection. </summary>
    /// <param name="connectionString"> The connection string. </param>
    /// <returns>   The new connection. </returns>
    public IDbConnection CreateConnection(string connectionString)
    {
        return new SqliteConnection(connectionString);
    }
}