using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluiTec.AppFx.Data.Sql.Adapters;

namespace FluiTec.AppFx.Data.Sql
{
    /// <summary>	A SQL builder. </summary>
    public class SqlBuilder
    {
        #region Constructors

        /// <summary>	Constructor. </summary>
        /// <param name="adapter">	The adapter. </param>
        public SqlBuilder(ISqlAdapter adapter)
        {
            Adapter = adapter;
        }

        #endregion

        #region Properties

        /// <summary>	The adapter. </summary>
        public ISqlAdapter Adapter { get; }

        #endregion

        #region Fields

        #region Cache

        /// <summary>	Options for controlling the key. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _keyParameters =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	List of parameters. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, List<KeyValuePair<PropertyInfo, string>>>
            _parameterList =
                new ConcurrentDictionary<RuntimeTypeHandle, List<KeyValuePair<PropertyInfo, string>>>();

        /// <summary>	The select all queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _selectAllQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	The select by key queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _selectByKeyQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	The select by filter queries. </summary>
        private readonly ConcurrentDictionary<KeyValuePair<RuntimeTypeHandle, string>, string> _selectByFilterQueries =
            new ConcurrentDictionary<KeyValuePair<RuntimeTypeHandle, string>, string>();

        /// <summary>	The insert automatic key queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _insertAutoKeyQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	The insert automatic multiple queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _insertAutoMultipleQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	The update queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _updateQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        /// <summary>	The delete queries. </summary>
        private readonly ConcurrentDictionary<RuntimeTypeHandle, string> _deleteQueries =
            new ConcurrentDictionary<RuntimeTypeHandle, string>();

        #endregion

        #endregion

        #region Statements

        /// <summary>	Select all. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string SelectAll(Type type)
        {
            // try to find statement in cache and return it
            if (_selectAllQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.SelectAllStatement(type);

            // add statement to cache
            _selectAllQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        /// <summary>	Select by key. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string SelectByKey(Type type)
        {
            // try to find statement in cache and return it
            if (_selectByKeyQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.GetByKeyStatement(type);

            // add statement to cache
            _selectByKeyQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        /// <summary>	Select by filter. </summary>
        /// <param name="type">			 	The type. </param>
        /// <param name="filterProperty">	The filter property. </param>
        /// <param name="selectFields">  	(Optional) The select fields. </param>
        /// <returns>	A string. </returns>
        public string SelectByFilter(Type type, string filterProperty, string[] selectFields = null)
        {
            var sqlKey = GenerateSqlKey(filterProperty, selectFields);
            // try to find statement in cache and return it
            if (_selectByFilterQueries.TryGetValue(new KeyValuePair<RuntimeTypeHandle, string>(type.TypeHandle, sqlKey),
                out var sql)) return sql;

            // generate statement
            sql = Adapter.GetByFilterStatement(type, filterProperty, selectFields);

            // add statement to cache
            _selectByFilterQueries.TryAdd(new KeyValuePair<RuntimeTypeHandle, string>(type.TypeHandle, sqlKey), sql);

            // return statement
            return sql;
        }

        /// <summary>	Select by filter. </summary>
        /// <param name="type">			   	The type. </param>
        /// <param name="filterProperties">	The filter properties. </param>
        /// <param name="selectFields">	   	(Optional) The select fields. </param>
        /// <returns>	A string. </returns>
        public string SelectByFilter(Type type, string[] filterProperties, string[] selectFields = null)
        {
            var sqlKey = $"{GenerateSqlKey(filterProperties)}:{selectFields}";
            // try to find statement in cache and return it
            if (_selectByFilterQueries.TryGetValue(new KeyValuePair<RuntimeTypeHandle, string>(type.TypeHandle, sqlKey),
                out var sql)) return sql;

            // generate statement
            sql = Adapter.GetByFilterStatement(type, filterProperties, selectFields);

            // add statement to cache
            _selectByFilterQueries.TryAdd(new KeyValuePair<RuntimeTypeHandle, string>(type.TypeHandle, sqlKey), sql);

            // return statement
            return sql;
        }

        /// <summary>	Inserts an automatic key described by type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string InsertAutoKey(Type type)
        {
            // try to find statement in cache and return it
            if (_insertAutoKeyQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.GetInsertAutoKeyStatement(type);

            // add statement to cache
            _insertAutoKeyQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        /// <summary>	Inserts an automatic multiple. </summary>
        /// <param name="type"> 	The type. </param>
        /// <returns>	A string. </returns>
        public string InsertAutoMultiple(Type type)
        {
            // try to find statement in cache and return it
            if (_insertAutoMultipleQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.GetInsertAutoKeyMultipleStatement(type);

            // add statement to cache
            _insertAutoMultipleQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        /// <summary>	Updates the given type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string Update(Type type)
        {
            // try to find statement in cache and return it
            if (_updateQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.GetUpdateStatement(type);

            // add statement to cache
            _updateQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        /// <summary>	Deletes the given type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string Delete(Type type)
        {
            // try to find statement in cache and return it
            if (_deleteQueries.TryGetValue(type.TypeHandle, out var sql)) return sql;

            // generate statement
            sql = Adapter.GetDeleteStatememt(type);

            // add statement to cache
            _deleteQueries.TryAdd(type.TypeHandle, sql);

            // return statement
            return sql;
        }

        #endregion

        #region Misc

        /// <summary>	Key parameter. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public string KeyParameter(Type type)
        {
            // try to find parameter in cache and return it
            if (_keyParameters.TryGetValue(type.TypeHandle, out var param)) return param;

            // generate param
            param = Adapter.GetKeyParameter(type);

            // add param to cache
            _keyParameters.TryAdd(type.TypeHandle, param);

            // return param
            return param;
        }

        /// <summary>	Parameter list. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A List&lt;KeyValuePair&lt;PropertyInfo,string&gt;&gt; </returns>
        public List<KeyValuePair<PropertyInfo, string>> ParameterList(Type type)
        {
            // try to find parameters in cache and return them
            if (_parameterList.TryGetValue(type.TypeHandle, out var paramList))
                return paramList;

            // generate paramList
            paramList = SqlCache.TypePropertiesChache(type).Select(p =>
                    new KeyValuePair<PropertyInfo, string>(p, Adapter.RenderParameterProperty(p)))
                .ToList();

            // add paramlist to cache
            _parameterList.TryAdd(type.TypeHandle, paramList);

            // return paramList
            return paramList;
        }

        #endregion

        #region Helpers

        /// <summary>	Generates a SQL key. </summary>
        /// <param name="filterProperty">	The filter property. </param>
        /// <param name="selectFields">  	(Optional) The select fields. </param>
        /// <returns>	The SQL key. </returns>
        private static string GenerateSqlKey(string filterProperty, IReadOnlyList<string> selectFields)
        {
            return $"{filterProperty.ToLower()}{GenerateSqlKey(selectFields)}";
        }

        /// <summary>	Generates a SQL key. </summary>
        /// <param name="selectFields">	(Optional) The select fields. </param>
        /// <returns>	The SQL key. </returns>
        private static string GenerateSqlKey(IReadOnlyList<string> selectFields)
        {
            if (selectFields == null || selectFields.Count < 1)
                return "*";
            var sb = new StringBuilder();
            for (var i = 0; i < selectFields.Count; i++)
            {
                if (i > 0)
                    sb.Append(';');
                sb.Append(selectFields[i].ToLower());
            }

            return sb.ToString();
        }

        #endregion
    }
}