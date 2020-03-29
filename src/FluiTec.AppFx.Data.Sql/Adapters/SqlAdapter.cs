using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluiTec.AppFx.Data.Sql.Mappers;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    /// <summary>	A SQL adapter. </summary>
    public abstract class SqlAdapter : ISqlAdapter
    {
        /// <summary>	The entity name mapper. </summary>
        protected readonly IEntityNameMapper EntityNameMapper;

        /// <summary>	Specialised default constructor for use only by derived class. </summary>
        protected SqlAdapter() : this(new AttributeTypeEntityNameMapper())
        {
        }

        /// <summary>	Specialised constructor for use only by derived class. </summary>
        /// <param name="entityNameMapper">	The entity name mapper. </param>
        protected SqlAdapter(IEntityNameMapper entityNameMapper)
        {
            EntityNameMapper = entityNameMapper ?? throw new ArgumentNullException(nameof(entityNameMapper));
        }

        #region Parameters

        /// <summary>	Gets key parameter. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The key parameter. </returns>
        public virtual string GetKeyParameter(Type type)
        {
            var key = SqlCache.TypeKeyPropertiesCache(type).Single();
            return RenderParameterProperty(key);
        }

        #endregion

        #region Statements

        /// <summary>	The select all statement. </summary>
        public virtual string SelectAllStatement(Type type)
        {
            return
                $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)}";
        }

        /// <summary>	Gets by identifier statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The by identifier statement. </returns>
        public virtual string GetByKeyStatement(Type type)
        {
            var key = SqlCache.TypeKeyPropertiesCache(type).Single();
            return
                $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)} WHERE {RenderPropertyName(key)} = {RenderParameterProperty(key)}";
        }

        /// <summary>	Gets by filter statement. </summary>
        /// <param name="type">			 	The type. </param>
        /// <param name="filterProperty">	The filter property. </param>
        /// <param name="selectFields">  	The select fields. </param>
        /// <returns>	The by filter statement. </returns>
        public virtual string GetByFilterStatement(Type type, string filterProperty, string[] selectFields)
        {
            var fProp = SqlCache.TypePropertiesChache(type).Single(pi => pi.Name == filterProperty);
            if (selectFields == null || selectFields.Length < 1)
                return
                    $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)} WHERE {RenderPropertyName(fProp)} = {RenderParameterProperty(fProp)}";

            var sProps = SqlCache.TypePropertiesChache(type).Where(pi => selectFields.Contains(pi.Name)).ToArray();
            return
                $"SELECT {RenderPropertyList(sProps)} FROM {RenderTableName(type)} WHERE {RenderPropertyName(fProp)} = {RenderParameterProperty(fProp)}";
        }

        /// <summary>	Gets by filter statement. </summary>
        /// <param name="type">			   	The type. </param>
        /// <param name="filterProperties">	The filter properties. </param>
        /// <param name="selectFields">	   	The select fields. </param>
        /// <returns>	The by filter statement. </returns>
        public virtual string GetByFilterStatement(Type type, string[] filterProperties, string[] selectFields)
        {
            var fProps = SqlCache.TypePropertiesChache(type).Where(pi => filterProperties.Contains(pi.Name)).ToArray();
            var sb = new StringBuilder();
            for (var i = 0; i < filterProperties.Length; i++)
            {
                if (i > 0)
                    sb.Append(" AND ");
                sb.Append($"{RenderPropertyName(fProps[i])} = {RenderParameterProperty(fProps[i])}");
            }

            var filterSql = sb.ToString();

            if (selectFields == null || selectFields.Length < 1)
                return
                    $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)} WHERE {filterSql}";

            var sProps = SqlCache.TypePropertiesChache(type).Where(pi => selectFields.Contains(pi.Name)).ToArray();
            return
                $"SELECT {RenderPropertyList(sProps)} FROM {RenderTableName(type)} WHERE {filterSql}";
        }

        /// <summary>	Gets insert automatic key statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The insert automatic key statement. </returns>
        public virtual string GetInsertAutoKeyStatement(Type type)
        {
            var key = SqlCache.TypeKeyPropertiesCache(type).Single();
            var columnList = RenderAutoKeyColumnList(type).ToString();
            var parameterList = RenderAutoKeyParameterList(type).ToString();

            return
                $"INSERT INTO {RenderTableName(type)} ({columnList}) VALUES ({parameterList}){GetAutoKeyStatement(key)}";
        }

        /// <summary>	Gets insert automatic key multiple statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The insert automatic key multiple statement. </returns>
        public string GetInsertAutoKeyMultipleStatement(Type type)
        {
            var columnList = RenderAutoKeyColumnList(type).ToString();
            var parameterList = RenderAutoKeyParameterList(type).ToString();

            return $"INSERT INTO {RenderTableName(type)} ({columnList}) VALUES ({parameterList})";
        }

        /// <summary>	Gets automatic key statement. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	The automatic key statement. </returns>
        public abstract string GetAutoKeyStatement(PropertyInfo propertyInfo);

        /// <summary>	Gets update statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The update statement. </returns>
        public virtual string GetUpdateStatement(Type type)
        {
            var key = SqlCache.TypeKeyPropertiesCache(type).Single();
            var setClauses = RenderSetStatements(type).ToString();
            return
                $"UPDATE {RenderTableName(type)} " +
                $"SET {setClauses} " +
                $"WHERE {RenderPropertyName(key)} = {RenderParameterProperty(key)}";
        }

        /// <summary>	Gets delete statememt. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The delete statememt. </returns>
        public virtual string GetDeleteStatememt(Type type)
        {
            var key = SqlCache.TypeKeyPropertiesCache(type).Single();
            return
                $"DELETE FROM {RenderTableName(type)} WHERE {RenderPropertyName(key)} = {RenderParameterProperty(key)}";
        }

        #endregion

        #region Rendering

        /// <summary>	Renders the property list described by properties. </summary>
        /// <param name="properties">	The properties. </param>
        /// <returns>	A string. </returns>
        public virtual StringBuilder RenderPropertyList(PropertyInfo[] properties)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < properties.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(RenderPropertyName(properties[i]));
            }

            return sb;
        }

        /// <summary>	Renders the table name described by tableName. </summary>
        /// <param name="tableName">	Name of the table. </param>
        /// <returns>	A string. </returns>
        public virtual string RenderTableName(string tableName)
        {
            return tableName;
        }

        /// <summary>	Renders the table name described by type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A string. </returns>
        public virtual string RenderTableName(Type type)
        {
            return RenderTableName(EntityNameMapper.GetName(type));
        }

        /// <summary>	Renders the property name described by propertyInfo. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	A string. </returns>
        public virtual string RenderPropertyName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }

        /// <summary>Renders the property name described by propertyName.</summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <returns>A string.</returns>
        public virtual string RenderPropertyName(string propertyName)
        {
            return propertyName;
        }

        /// <summary>	Renders the parameter property described by propertyInfo. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	A string. </returns>
        public virtual string RenderParameterProperty(PropertyInfo propertyInfo)
        {
            return $"@{propertyInfo.Name}";
        }

        /// <summary>	Renders the column list described by type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A StringBuilder. </returns>
        public virtual StringBuilder RenderAutoKeyColumnList(Type type)
        {
            var propertiesWithoutKey = GetPropertiesWithoutKey(type).ToArray();
            return RenderPropertyList(propertiesWithoutKey);
        }

        /// <summary>	Renders the parameter list described by type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A StringBuilder. </returns>
        public virtual StringBuilder RenderAutoKeyParameterList(Type type)
        {
            var sb = new StringBuilder();
            var propertiesWithoutKey = GetPropertiesWithoutKey(type).ToArray();
            for (var i = 0; i < propertiesWithoutKey.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(RenderParameterProperty(propertiesWithoutKey[i]));
            }

            return sb;
        }

        /// <summary>	Renders the set statements described by type. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	A StringBuilder. </returns>
        public virtual StringBuilder RenderSetStatements(Type type)
        {
            var sb = new StringBuilder();
            var propertiesExceptKey = GetPropertiesWithoutKey(type).ToArray();
            for (var i = 0; i < propertiesExceptKey.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(
                    $"{RenderPropertyName(propertiesExceptKey[i])} = {RenderParameterProperty(propertiesExceptKey[i])}");
            }

            return sb;
        }

        /// <summary>	Gets the columns without keys in this collection. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the columns without keys in this
        ///     collection.
        /// </returns>
        public virtual IEnumerable<PropertyInfo> GetPropertiesWithoutKey(Type type)
        {
            return SqlCache.TypePropertiesChache(type).Except(new[] {SqlCache.TypeKeyPropertiesCache(type).Single()});
        }

        #endregion
    }
}