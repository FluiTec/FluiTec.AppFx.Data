using System.Data;
using MySql.Data.MySqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mysql
{
    /// <summary>	A mysql connection factory. </summary>
    public class MysqlConnectionFactory : IConnectionFactory
    {
        /// <summary>	Creates a connection. </summary>
        /// <param name="connectionString">	The connection string. </param>
        /// <returns>	The new connection. </returns>
        public IDbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}