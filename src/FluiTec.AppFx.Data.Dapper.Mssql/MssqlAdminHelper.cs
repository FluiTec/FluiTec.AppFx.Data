using Microsoft.Data.SqlClient;

namespace FluiTec.AppFx.Data.Dapper.Mssql;

/// <summary>   A mssql admin helper.</summary>
public static class MssqlAdminHelper
{
    /// <summary>   Creates a dababase.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    public static void CreateDababase(string adminConnectionString, string dbName)
    {
        using var connection = new SqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                              $"CREATE DATABASE {dbName};";
            using var createDbCmd = new SqlCommand(createDbSql, connection);
            createDbCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
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
            createUserCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }
}