using System;
using System.Collections.Concurrent;
using System.Data;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Sql.Adapters;

namespace FluiTec.AppFx.Data.Sql;

/// <summary>	A default SQL builder. </summary>
public static class DefaultSqlBuilder
{
    /// <summary>
    ///     Gets SQL builder by database delegate.
    /// </summary>
    /// <param name="sqlType">  Type of the SQL. </param>
    /// <returns>
    ///     A SqlBuilder.
    /// </returns>
    public delegate SqlBuilder GetSqlBuilderByDbDelegate(SqlType sqlType);

    /// <summary>	Gets SQL builder delegate. </summary>
    /// <param name="connection">	The connection. </param>
    /// <returns>	A SqlBuilder. </returns>
    public delegate SqlBuilder GetSqlBuilderDelegate(IDbConnection connection);

    /// <summary>
    ///     (Immutable) The padlock.
    /// </summary>
    private static readonly object Padlock = new();

    /// <summary>	Dictionary of builders. </summary>
    private static readonly ConcurrentDictionary<string, SqlBuilder> ConnectionBuilderDictionary;

    /// <summary>	Dictionary of builders. </summary>
    private static readonly ConcurrentDictionary<SqlType, SqlBuilder> SqlTypeBuilderDictionary;

    /// <summary>	The get SQL builder. </summary>
    public static GetSqlBuilderDelegate GetSqlBuilder;

    /// <summary>
    ///     The get SQL by database builder.
    /// </summary>
    public static GetSqlBuilderByDbDelegate GetSqlByDbBuilder;

    /// <summary>	Static constructor. </summary>
    static DefaultSqlBuilder()
    {
        ConnectionBuilderDictionary = new ConcurrentDictionary<string, SqlBuilder>();
        ConnectionBuilderDictionary.TryAdd("Microsoft.Data.SqlClient.SqlConnection",
            new SqlBuilder(new MicrosoftSqlAdapter(new AttributeEntityNameService())));
        ConnectionBuilderDictionary.TryAdd("System.Data.SqlClient.SqlConnection",
            new SqlBuilder(new MicrosoftSqlAdapter(new AttributeEntityNameService())));
        ConnectionBuilderDictionary.TryAdd("Npgsql.NpgsqlConnection",
            new SqlBuilder(new PostgreSqlAdapter(new AttributeEntityNameService())));
        ConnectionBuilderDictionary.TryAdd("MySql.Data.MySqlClient.MySqlConnection",
            new SqlBuilder(new MySqlAdapter(new AttributeEntityNameService())));
        ConnectionBuilderDictionary.TryAdd("Microsoft.Data.Sqlite.SqliteConnection",
            new SqlBuilder(new SqLiteAdapter(new AttributeEntityNameService())));

        SqlTypeBuilderDictionary = new ConcurrentDictionary<SqlType, SqlBuilder>();
        SqlTypeBuilderDictionary.TryAdd(SqlType.Mssql,
            new SqlBuilder(new MicrosoftSqlAdapter(new AttributeEntityNameService())));
        SqlTypeBuilderDictionary.TryAdd(SqlType.Pgsql,
            new SqlBuilder(new PostgreSqlAdapter(new AttributeEntityNameService())));
        SqlTypeBuilderDictionary.TryAdd(SqlType.Mysql,
            new SqlBuilder(new MySqlAdapter(new AttributeEntityNameService())));
        SqlTypeBuilderDictionary.TryAdd(SqlType.Sqlite,
            new SqlBuilder(new SqLiteAdapter(new AttributeEntityNameService())));
    }

    /// <summary>	An IDbConnection extension method that gets a builder. </summary>
    /// <param name="connection">	The connection to act on. </param>
    /// <returns>	The builder. </returns>
    public static SqlBuilder GetBuilder(this IDbConnection connection)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        lock (Padlock)
        {
            if (GetSqlBuilder != null) return GetSqlBuilder(connection);
            var connectionTypeName = connection.GetType().FullName;
            if (ConnectionBuilderDictionary.TryGetValue(connectionTypeName ?? throw new InvalidOperationException(),
                    out var builder))
                return builder;
            throw new NotImplementedException(
                $"No default builder for connection for {connectionTypeName} registered!");
        }
    }

    /// <summary>
    ///     An IDbConnection extension method that gets a builder.
    /// </summary>
    /// <param name="dbType">   The dbType to act on. </param>
    /// <returns>
    ///     The builder.
    /// </returns>
    public static SqlBuilder GetBuilder(this SqlType dbType)
    {
        lock (Padlock)
        {
            if (GetSqlByDbBuilder != null) return GetSqlByDbBuilder(dbType);
            if (SqlTypeBuilderDictionary.TryGetValue(dbType, out var builder))
                return builder;
            throw new NotImplementedException(
                $"No default builder for DbType for {dbType} registered!");
        }
    }
}