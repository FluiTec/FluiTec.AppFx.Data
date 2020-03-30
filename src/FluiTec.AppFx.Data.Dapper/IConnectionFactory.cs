using System.Data;

namespace FluiTec.AppFx.Data.Dapper
{
    public interface IConnectionFactory
    {
        /// <summary>	Creates a connection. </summary>
        /// <param name="connectionString">	The connection string. </param>
        /// <returns>	The new connection. </returns>
        IDbConnection CreateConnection(string connectionString);
    }
}