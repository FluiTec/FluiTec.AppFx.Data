using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace FluiTec.AppFx.Data.Sql
{
    /// <summary>	A SQL extensions. </summary>
    public static class SqlExtensions
    {
        /// <summary>	Event queue for all listeners interested in SqlGenerated events. </summary>
        public static event EventHandler<SqlEventArgs> SqlGenerated;

        /// <summary>	Gets all items in this collection. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all items in this collection.
        /// </returns>
        public static IEnumerable<TEntity> GetAll<TEntity>(this IDbConnection connection,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sql = connection.GetBuilder().SelectAll(typeof(TEntity));
            OnSqlGenerated(sql);
            return connection.Query<TEntity>(sql, null, transaction, commandTimeout: commandTimeout);
        }

        /// <summary>	An IDbConnection extension method that gets. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="id">			 	The identifier. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>	A TEntity. </returns>
        public static TEntity Get<TEntity>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null,
            int? commandTimeout = null) where TEntity : class
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.SelectByKey(type);
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            parameters.Add(builder.KeyParameter(type), id);
            return connection.Query<TEntity>(sql, parameters, transaction, commandTimeout: commandTimeout)
                .SingleOrDefault();
        }

        /// <summary>	An IDbConnection extension method that inserts. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="entity">		 	The entity. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>	A long. </returns>
        public static long InsertAuto<TEntity>(this IDbConnection connection, TEntity entity,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertAutoKey(type);
            OnSqlGenerated(sql);

            var multi = connection.QueryMultiple(sql, entity, transaction, commandTimeout);

            var result = multi.Read().FirstOrDefault();
            if (result?.Id == null) return 0;

            var id = (int) result.Id;

            var keyProperty = SqlCache.TypeKeyPropertiesCache(type).Single();
            keyProperty.SetValue(entity, id);

            return id;
        }

        /// <summary>	An IDbConnection extension method that inserts an automatic multiple. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="entities">		 	The entities. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>	A long. </returns>
        public static long InsertAutoMultiple<TEntity>(this IDbConnection connection, IEnumerable<TEntity> entities,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertAutoMultiple(type);
            OnSqlGenerated(sql);

            // just return number of affected rows instead of the indivitual id's
            return connection.Execute(sql, entities, transaction, commandTimeout);
        }

        /// <summary>	An IDbConnection extension method that updates this object. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="entity">		 	The entity. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>	True if it succeeds, false if it fails. </returns>
        public static bool Update<TEntity>(this IDbConnection connection, TEntity entity,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Update(type);
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            foreach (var p in builder.ParameterList(type))
                parameters.Add(p.Value, p.Key.GetValue(entity));

            return connection.Execute(sql, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>	An IDbConnection extension method that deletes this object. </summary>
        /// <typeparam name="TEntity">	Type of the entity. </typeparam>
        /// <param name="connection">	 	The connection to act on. </param>
        /// <param name="id">			 	The identifier. </param>
        /// <param name="transaction">   	(Optional) The transaction. </param>
        /// <param name="commandTimeout">	(Optional) The command timeout. </param>
        /// <returns>	True if it succeeds, false if it fails. </returns>
        public static bool Delete<TEntity>(this IDbConnection connection, dynamic id, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Delete(type);
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            parameters.Add(builder.KeyParameter(type), id);

            return connection.Execute(sql, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>	Executes the SQL generated action. </summary>
        /// <param name="sql">	The SQL. </param>
        private static void OnSqlGenerated(string sql)
        {
            SqlGenerated?.Invoke(null, new SqlEventArgs(sql));
        }
    }
}