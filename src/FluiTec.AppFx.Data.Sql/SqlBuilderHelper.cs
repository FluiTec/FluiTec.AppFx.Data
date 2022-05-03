using System;
using System.Data;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Sql.Adapters;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Sql;

/// <summary>	A default SQL builder. </summary>
public static class SqlBuilderHelper
{
    /// <summary>
    /// An IDbConnection extension method that gets a builder.
    /// </summary>
    ///
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    ///
    /// <param name="connection">       The connection to act on. </param>
    /// <param name="nameService">      The name service. </param>
    /// <param name="loggerFactory">    The logger. </param>
    ///
    /// <returns>
    /// The builder.
    /// </returns>
    public static ISqlBuilder GetBuilder(this IDbConnection connection, IEntityNameService nameService,
        ILoggerFactory loggerFactory)
    {
        if (connection == null)
            throw new ArgumentNullException(nameof(connection));

        return connection.GetType().FullName switch
        {
            "System.Data.SqlClient.SqlConnection" => SqlType.Mssql.GetBuilder(nameService, loggerFactory),
            "Microsoft.Data.SqlClient.SqlConnection" => SqlType.Mssql.GetBuilder(nameService, loggerFactory),
            "Npgsql.NpgsqlConnection" => SqlType.Pgsql.GetBuilder(nameService, loggerFactory),
            "MySql.Data.MySqlClient.MySqlConnection" => SqlType.Mysql.GetBuilder(nameService, loggerFactory),
            "Microsoft.Data.Sqlite.SqliteConnection" => SqlType.Sqlite.GetBuilder(nameService, loggerFactory),
            _ => throw new NotImplementedException(
                $"No default builder for connection for {connection.GetType().FullName} registered!")
        };
    }

    /// <summary>
    /// An IDbConnection extension method that gets a builder.
    /// </summary>
    ///
    /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
    ///                                                 the required range. </exception>
    ///
    /// <param name="dbType">           The dbType to act on. </param>
    /// <param name="nameService">      The name service. </param>
    /// <param name="loggerFactory">    The logger. </param>
    ///
    /// <returns>
    /// The builder.
    /// </returns>
    public static ISqlBuilder GetBuilder(this SqlType dbType, IEntityNameService nameService,
        ILoggerFactory loggerFactory)
    {
        return dbType switch
        {
            SqlType.Mssql => new SqlBuilder(
                new MicrosoftSqlAdapter(nameService, loggerFactory?.CreateLogger<ISqlAdapter>()),
                loggerFactory?.CreateLogger<ISqlBuilder>()),
            SqlType.Mysql => new SqlBuilder(new MySqlAdapter(nameService, loggerFactory?.CreateLogger<ISqlAdapter>()),
                loggerFactory?.CreateLogger<ISqlBuilder>()),
            SqlType.Pgsql => new SqlBuilder(
                new PostgreSqlAdapter(nameService, loggerFactory?.CreateLogger<ISqlAdapter>()),
                loggerFactory?.CreateLogger<ISqlBuilder>()),
            SqlType.Sqlite => new SqlBuilder(new SqLiteAdapter(nameService, loggerFactory?.CreateLogger<ISqlAdapter>()),
                loggerFactory?.CreateLogger<ISqlBuilder>()),
            _ => throw new ArgumentOutOfRangeException(nameof(dbType), dbType, null)
        };
    }
}