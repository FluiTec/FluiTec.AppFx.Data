using System;
using MySql.Data.MySqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mysql;

/// <summary>   A mysql admin helper.</summary>
public static class MysqlAdminHelper
{
    // <summary>   Creates a dababase.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    public static void CreateDababase(string adminConnectionString, string dbName)
    {
        System.Console.WriteLine($"LOG:MysqlAdminHelper:CreateDatabase - ConStr: {adminConnectionString}, DB: {dbName}");
        using var connection = new MySqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                              $"CREATE DATABASE {dbName};";
            using var createDbCmd = new MySqlCommand(createDbSql, connection);
            var result = createDbCmd.ExecuteNonQuery();
            System.Console.WriteLine($"LOG:MysqlAdminHelper:CreateDatabase - RESULT {result}");
        }
        finally
        {
            connection.Close();
            System.Console.WriteLine("LOG:MysqlAdminHelper:CreateDatabase - FAULTED");
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
        using var connection = new MySqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createUserSql =
                $"DELETE FROM mysql.db WHERE User='{userName}';{Environment.NewLine}" +
                $"FLUSH PRIVILEGES;{Environment.NewLine}" +
                $"CREATE USER IF NOT EXISTS {userName}@'%' IDENTIFIED BY '{password}';{Environment.NewLine}" +
                $"FLUSH PRIVILEGES;{Environment.NewLine}" +
                $"GRANT ALL PRIVILEGES ON *.* TO {userName}@'%';{Environment.NewLine}" +
                $"FLUSH PRIVILEGES;{Environment.NewLine}";
            using var createUserCmd = new MySqlCommand(createUserSql, connection);
            var unused = createUserCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }
}