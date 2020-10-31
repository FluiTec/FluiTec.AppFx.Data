using MySql.Data.MySqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mysql
{
    /// <summary>   A mysql admin helper.</summary>
    public static class MysqlAdminHelper
    {
        // <summary>   Creates a dababase.</summary>
        /// <param name="adminConnectionString">    The admin connection string. </param>
        /// <param name="dbName">                   Name of the database. </param>
        public static void CreateDababase(string adminConnectionString, string dbName)
        {
            using (var connection = new MySqlConnection(adminConnectionString))
            {
                try
                {
                    connection.Open();
                    var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                                      $"CREATE DATABASE {dbName};";
                    using (var createDbCmd = new MySqlCommand(createDbSql, connection))
                        createDbCmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>   Creates user and login.</summary>
        /// <param name="adminConnectionString">    The admin connection string. </param>
        /// <param name="dbName">                   Name of the database. </param>
        /// <param name="userName">                 Name of the user. </param>
        /// <param name="password">                 The password. </param>
        public static void CreateUserAndLogin(string adminConnectionString, string dbName, string userName,
            string password)
        {
            using (var connection = new MySqlConnection(adminConnectionString))
            {
                try
                {
                    connection.Open();
                    var createUserSql = $"GRANT ALL ON `{dbName}`.* TO '{userName}'@'%' IDENTIFIED BY '{password}';";
                    using (var createUserCmd = new MySqlCommand(createUserSql, connection))
                        createUserCmd.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
