using System.Data;
using System.Data.SqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mssql
{
    /// <summary>	A mssql connection factory. </summary>
    public class MssqlConnectionFactory : IConnectionFactory
    {
        /// <summary>	Creates a connection. </summary>
        /// <param name="connectionString">	The connection string. </param>
        /// <returns>	The new connection. </returns>
        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}