using System;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Sql.Adapters;

namespace FluiTec.AppFx.Data.Sql
{
    /// <summary>	A default SQL builder. </summary>
    public static class DefaultSqlBuilder
    {
        /// <summary>	Gets SQL builder delegate. </summary>
        /// <param name="connection">	The connection. </param>
        /// <returns>	A SqlBuilder. </returns>
        public delegate SqlBuilder GetSqlBuilderDelegate(IDbConnection connection);

        /// <summary>	The padlock. </summary>
        private static readonly object Padlock = new object();

        /// <summary>	Dictionary of builders. </summary>
        private static readonly ConcurrentDictionary<string, SqlBuilder> BuilderDictionary;

        /// <summary>	The get SQL builder. </summary>
        public static GetSqlBuilderDelegate GetSqlBuilder;

        /// <summary>	Static constructor. </summary>
        static DefaultSqlBuilder()
        {
            BuilderDictionary = new ConcurrentDictionary<string, SqlBuilder>();
            BuilderDictionary.TryAdd("Microsoft.Data.SqlClient.SqlConnection",
                new SqlBuilder(new MicrosoftSqlAdapter(new AttributeEntityNameService())));
            BuilderDictionary.TryAdd("System.Data.SqlClient.SqlConnection",
                new SqlBuilder(new MicrosoftSqlAdapter(new AttributeEntityNameService())));
            BuilderDictionary.TryAdd("Npgsql.NpgsqlConnection",
                new SqlBuilder(new PostgreSqlAdapter(new AttributeEntityNameService())));
            BuilderDictionary.TryAdd("MySql.Data.MySqlClient.MySqlConnection",
                new SqlBuilder(new MySqlAdapter(new AttributeEntityNameService())));
            BuilderDictionary.TryAdd("Microsoft.Data.Sqlite.SqliteConnection",
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
                Debug.Assert(connectionTypeName != null);
                if (BuilderDictionary.TryGetValue(connectionTypeName, out var builder))
                    return builder;
                throw new NotImplementedException(
                    $"No default builder for connection for {connectionTypeName} registered!");
            }
        }
    }
}