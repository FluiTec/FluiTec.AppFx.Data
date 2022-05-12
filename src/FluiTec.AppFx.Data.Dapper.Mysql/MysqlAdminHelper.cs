using System;
using MySql.Data.MySqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mysql;

/// <summary>   A mysql admin helper.</summary>
public class MysqlAdminHelper : IAdminHelper
{
    /// <summary>   Creates user and login.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    /// <param name="userName">                 Name of the user. </param>
    /// <param name="password">                 The password. </param>
    public static int CreateUserAndLogin(string adminConnectionString, string dbName, string userName,
        string password)
    {
        var result = -1;
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
            result = createUserCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }

        return result;
    }

    // <summary>   Creates a dababase.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    public static int CreateDababase(string adminConnectionString, string dbName)
    {
        var result = -1;
        System.Console.WriteLine($"LOG:AdminHelper:CreateDatabase - ConStr: {adminConnectionString}, DB: {dbName}");
        using var connection = new MySqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                              $"CREATE DATABASE {dbName};";
            using var createDbCmd = new MySqlCommand(createDbSql, connection);
            result = createDbCmd.ExecuteNonQuery();
            System.Console.WriteLine($"LOG:AdminHelper:CreateDatabase - RESULT {result}");
        }
        catch (Exception)
        {
            System.Console.WriteLine("LOG:AdminHelper:CreateDatabase - FAULTED");
        }
        finally
        {
            connection.Close();
        }

        return result;
    }
}