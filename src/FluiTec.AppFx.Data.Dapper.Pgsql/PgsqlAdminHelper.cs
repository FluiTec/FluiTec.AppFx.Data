using Npgsql;

namespace FluiTec.AppFx.Data.Dapper.Pgsql;

/// <summary>   A mssql admin helper.</summary>
public static class PgsqlAdminHelper
{
    /// <summary>   Creates a dababase.</summary>
    /// <param name="adminConnectionString">    The admin connection string. </param>
    /// <param name="dbName">                   Name of the database. </param>
    public static void CreateDababase(string adminConnectionString, string dbName)
    {
        using var connection = new NpgsqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createDbSql = $"DROP DATABASE IF EXISTS {dbName};\r\n" +
                              $"CREATE DATABASE {dbName};";
            using var createDbCmd = new NpgsqlCommand(createDbSql, connection);
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
        using var connection = new NpgsqlConnection(adminConnectionString);
        try
        {
            connection.Open();
            var createUserSql = "DO\r\n" +
                                "$do$\r\n" +
                                "BEGIN\r\n" +
                                $"   IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = '{userName}') \r\n" +
                                "   THEN\r\n" +
                                $"      CREATE ROLE {userName} LOGIN PASSWORD '{password}';\r\n" +
                                "   END IF;\r\n" +
                                "END\r\n" +
                                "$do$;\r\n" +
                                $"ALTER DATABASE {dbName} OWNER TO {userName};";
            using var createUserCmd = new NpgsqlCommand(createUserSql, connection);
            createUserCmd.ExecuteNonQuery();
        }
        finally
        {
            connection.Close();
        }
    }
}