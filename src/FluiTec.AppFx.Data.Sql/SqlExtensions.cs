﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Entities;

// ReSharper disable UnusedMember.Global

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

        /// <summary>   An IDbConnection extension method that gets all asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   all async&lt; t entity&gt;</returns>
        public static Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(this IDbConnection connection,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var sql = connection.GetBuilder().SelectAll(typeof(TEntity));
            OnSqlGenerated(sql);
            return connection.QueryAsync<TEntity>(sql, null, transaction, commandTimeout);
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
            return connection.QuerySingleOrDefault<TEntity>(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>   An IDbConnection extension method that gets an asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="id">               The identifier. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   The async&lt; t entity&gt;</returns>
        public static Task<TEntity> GetAsync<TEntity>(this IDbConnection connection, dynamic id,
            IDbTransaction transaction = null,
            int? commandTimeout = null) where TEntity : class
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.SelectByKey(type);
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            parameters.Add(builder.KeyParameter(type), id);
            return connection.QuerySingleOrDefaultAsync<TEntity>(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>   An IDbConnection extension method that inserts.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entity">           The entity. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        public static void Insert<TEntity>(this IDbConnection connection, TEntity entity,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Insert(type);
            OnSqlGenerated(sql);

            connection.Execute(sql, entity, transaction, commandTimeout);
        }

        /// <summary>   An IDbConnection extension method that inserts an asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entity">           The entity. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   An asynchronous result.</returns>
        public static async Task InsertAsync<TEntity>(this IDbConnection connection, TEntity entity,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Insert(type);
            OnSqlGenerated(sql);

            await connection.ExecuteAsync(sql, entity, transaction, commandTimeout);
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

        /// <summary>An IDbConnection extension method that inserts an automatic asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entity">           The entity. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   A long.</returns>
        public static async Task<long> InsertAutoAsync<TEntity>(this IDbConnection connection, TEntity entity,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertAutoKey(type);
            OnSqlGenerated(sql);

            var multi = await connection.QueryMultipleAsync(sql, entity, transaction, commandTimeout);

            var result = (await multi.ReadAsync()).FirstOrDefault();
            if (result?.Id == null) return 0;

            var id = (int) result.Id;

            var keyProperty = SqlCache.TypeKeyPropertiesCache(type).Single();
            keyProperty.SetValue(entity, id);

            return id;
        }

        /// <summary>   An IDbConnection extension method that inserts a multiple.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entities">         The entities. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   A long.</returns>
        public static long InsertMultiple<TEntity>(this IDbConnection connection, IEnumerable<TEntity> entities,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertMultiple(type);
            OnSqlGenerated(sql);

            // just return number of affected rows instead of the indivitual id's
            return connection.Execute(sql, entities, transaction, commandTimeout);
        }

        /// <summary>An IDbConnection extension method that inserts a multiple asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entities">         The entities. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   A long.</returns>
        public static Task<int> InsertMultipleAsync<TEntity>(this IDbConnection connection,
            IEnumerable<TEntity> entities,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertMultiple(type);
            OnSqlGenerated(sql);

            // just return number of affected rows instead of the indivitual id's
            return connection.ExecuteAsync(sql, entities, transaction, commandTimeout);
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

        /// <summary>An IDbConnection extension method that inserts an automatic multiple asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entities">         The entities. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   A long.</returns>
        public static Task<int> InsertAutoMultipleAsync<TEntity>(this IDbConnection connection,
            IEnumerable<TEntity> entities,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.InsertAutoMultiple(type);
            OnSqlGenerated(sql);

            // just return number of affected rows instead of the indivitual id's
            return connection.ExecuteAsync(sql, entities, transaction, commandTimeout);
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

        /// <summary>   An IDbConnection extension method that updates the asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="entity">           The entity. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   True if it succeeds, false if it fails.</returns>
        public static async Task<bool> UpdateAsync<TEntity>(this IDbConnection connection, TEntity entity,
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

            return await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>   An IDbConnection extension method that updates this object. </summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">           The connection to act on. </param>
        /// <param name="entity">               The entity. </param>
        /// <param name="originalTimeStamp">    The original time stamp. </param>
        /// <param name="transaction">          (Optional) The transaction. </param>
        /// <param name="commandTimeout">       (Optional) The command timeout. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public static bool Update<TEntity>(this IDbConnection connection, TEntity entity,
            DateTimeOffset originalTimeStamp,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Update(type, originalTimeStamp, nameof(ITimeStampedKeyEntity.TimeStamp));
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            foreach (var p in builder.ParameterList(type))
                if (p.Value == "@TimeStamp")
                {
                    if (!builder.Adapter.SupportsDateTimeOffset)
                    {
                        parameters.Add(p.Value, ((DateTimeOffset) p.Key.GetValue(entity)).UtcDateTime, DbType.DateTime);
                        parameters.Add("@OriginalTimeStamp", originalTimeStamp.UtcDateTime, DbType.DateTime);
                    }
                    else
                    {
                        parameters.Add(p.Value, p.Key.GetValue(entity), DbType.DateTimeOffset);
                        parameters.Add("@OriginalTimeStamp", originalTimeStamp, DbType.DateTimeOffset);
                    }
                }
                else
                {
                    parameters.Add(p.Value, p.Key.GetValue(entity));
                }

            return connection.Execute(sql, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>   An IDbConnection extension method that updates the asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">           The connection to act on. </param>
        /// <param name="entity">               The entity. </param>
        /// <param name="originalTimeStamp">    The original time stamp. </param>
        /// <param name="transaction">          (Optional) The transaction. </param>
        /// <param name="commandTimeout">       (Optional) The command timeout. </param>
        /// <returns>   True if it succeeds, false if it fails.</returns>
        public static async Task<bool> UpdateAsync<TEntity>(this IDbConnection connection, TEntity entity,
            DateTimeOffset originalTimeStamp,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Update(type, originalTimeStamp, nameof(ITimeStampedKeyEntity.TimeStamp));
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            foreach (var p in builder.ParameterList(type))
                if (p.Value == "@TimeStamp")
                {
                    if (!builder.Adapter.SupportsDateTimeOffset)
                        parameters.Add(p.Value, ((DateTimeOffset) p.Key.GetValue(entity)).UtcDateTime, DbType.DateTime);
                    else
                        parameters.Add(p.Value, p.Key.GetValue(entity), DbType.DateTimeOffset);
                }
                else
                {
                    parameters.Add(p.Value, p.Key.GetValue(entity));
                }

            return await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout) > 0;
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

        /// <summary>   An IDbConnection extension method that deletes the asynchronous.</summary>
        /// <typeparam name="TEntity">  Type of the entity. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="id">               The identifier. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="commandTimeout">   (Optional) The command timeout. </param>
        /// <returns>   True if it succeeds, false if it fails.</returns>
        public static async Task<bool> DeleteAsync<TEntity>(this IDbConnection connection, dynamic id,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            var type = typeof(TEntity);

            var builder = connection.GetBuilder();
            var sql = builder.Delete(type);
            OnSqlGenerated(sql);

            var parameters = new DynamicParameters();
            parameters.Add(builder.KeyParameter(type), id);

            return await connection.ExecuteAsync(sql, parameters, transaction, commandTimeout) > 0;
        }

        /// <summary>	Executes the SQL generated action. </summary>
        /// <param name="sql">	The SQL. </param>
        private static void OnSqlGenerated(string sql)
        {
            SqlGenerated?.Invoke(null, new SqlEventArgs(sql));
        }
    }
}