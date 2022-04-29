using System;
using Microsoft.Data.SqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mssql;

/// <summary>   A mssql admin helper.</summary>
public class MssqlAdminHelper : IAdminHelper
{
    /// <summary>   Creates a dababase.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    public static int CreateDababase(string adminConnectionString, string dbName)
    {
        var result = -1;
        System.Console.WriteLine($"LOG:AdminHelper:CreateDatabase - ConStr: {adminConnectionString}, DB: {dbName}");
        using var connection = new SqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                              $"CREATE DATABASE {dbName};";
            using var createDbCmd = new SqlCommand(createDbSql, connection);
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

    /// <summary>   Creates user and login.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    /// <param name="userName">                 Name of the user. </param>
    /// <param name="password">                 The password. </param>
    public static int CreateUserAndLogin(string adminConnectionString, string dbName, string userName,
        string password)
    {
        var result = -1;
        using var connection = new SqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createUserSql = "USE master;\r\n" +
                                $"IF NOT EXISTS (SELECT loginname from master.dbo.syslogins where name = '{userName}' and dbname = 'master')\r\n" +
                                "BEGIN\r\n" +
                                $"  CREATE LOGIN [{userName}] WITH PASSWORD=N'{password}'\r\n" +
                                "END\r\n" +
                                $"USE {dbName};\r\n" +
                                $"IF NOT EXISTS(SELECT [name] FROM [sys].[database_principals] WHERE [name] = N'{userName}')\r\n" +
                                "BEGIN\r\n" +
                                $"CREATE USER [{userName}] FOR LOGIN [{userName}]\r\n" +
                                $"EXEC sp_addrolemember N'db_owner', N'{userName}'\r\n" +
                                "END";
            using var createUserCmd = new SqlCommand(createUserSql, connection);
            result = createUserCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }

        return result;
    }
}