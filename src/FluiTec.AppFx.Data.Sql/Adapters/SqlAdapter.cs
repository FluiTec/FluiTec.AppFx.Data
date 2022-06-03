using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluiTec.AppFx.Data.EntityNameServices;
using ImmediateReflection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Sql.Adapters;

/// <summary>	A SQL adapter. </summary>
public abstract class SqlAdapter : ISqlAdapter
{
    /// <summary>	The entity name mapper. </summary>
    protected readonly IEntityNameService EntityNameService;

    /// <summary>
    ///     Specialised constructor for use only by derived class.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="logger">               The logger. </param>
    protected SqlAdapter(IEntityNameService entityNameService, ILogger<ISqlAdapter> logger)
    {
        Logger = logger;
        EntityNameService = entityNameService ?? throw new ArgumentNullException(nameof(entityNameService));
    }

    /// <summary>
    ///     Gets the logger.
    /// </summary>
    /// <value>
    ///     The logger.
    /// </value>
    public ILogger<ISqlAdapter> Logger { get; }

    /// <summary>   Gets a value indicating whether the supports date time offset. </summary>
    /// <value> True if supports date time offset, false if not. </value>
    public abstract bool SupportsDateTimeOffset { get; }

    #region Parameters

    /// <summary>	Gets key parameter. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The key parameter. </returns>
    public virtual string GetKeyParameters(Type type)
    {
        var keys = SqlCache.TypeKeyPropertiesCache(type).ToArray();

        var sb = new StringBuilder();
        for (var i = 0; i < keys.Length; i++)
        {
            if (i > 0)
                sb.Append(", ");
            sb.Append(RenderParameterProperty(keys[i].PropertyInfo));
        }

        return sb.ToString();
    }

    #endregion

    #region Statements

    /// <summary>	The select all statement. </summary>
    public virtual string SelectAllStatement(Type type)
    {
        return
            $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)}";
    }

    /// <summary>
    /// Select paged statement.
    /// </summary>
    ///
    /// <param name="type">                         The type. </param>
    /// <param name="skipRecordCountParameterName"> Name of the skip record count parameter. </param>
    /// <param name="takeRecordCountParameterName"> Name of the take record count parameter. </param>
    ///
    /// <returns>
    /// A string.
    /// </returns>
    public virtual string SelectPagedStatement(Type type, string skipRecordCountParameterName, string takeRecordCountParameterName)
    {
        return $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} " +
               $"FROM {RenderTableName(type)} " +
               $"ORDER BY {RenderPropertyName(SqlCache.TypeKeyPropertiesCache(type).First().PropertyInfo)} " +
               $"OFFSET {RenderParameterPropertyName(skipRecordCountParameterName)} ROWS " +
               $"FETCH NEXT {RenderParameterPropertyName(takeRecordCountParameterName)} ROWS ONLY";
    }

    /// <summary>	Gets by identifier statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The by identifier statement. </returns>
    public virtual string GetByKeyStatement(Type type)
    {
        var keys = SqlCache.TypeKeyPropertiesCache(type).Select(k => k.PropertyInfo.Name).ToArray();
        var fields = SqlCache.TypePropertiesChache(type).Select(k => k.Name).ToArray();
        return GetByFilterStatement(type, keys, fields);
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

    /// <summary>   Gets by filter in statement.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <param name="collectionName">   Name of the collection. </param>
    /// <param name="selectFields">     The select fields. </param>
    /// <returns>   The by filter in statement.</returns>
    public virtual string GetByFilterInStatement(Type type, string filterProperty, string collectionName,
        string[] selectFields)
    {
        var fProp = SqlCache.TypePropertiesChache(type).Single(pi => pi.Name == filterProperty);
        if (selectFields == null || selectFields.Length < 1)
            return
                $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} FROM {RenderTableName(type)} WHERE {RenderInFilterByProperty(fProp, collectionName)}";

        var sProps = SqlCache.TypePropertiesChache(type).Where(pi => selectFields.Contains(pi.Name)).ToArray();
        return
            $"SELECT {RenderPropertyList(sProps)} FROM {RenderTableName(type)} WHERE {RenderInFilterByProperty(fProp, collectionName)}";
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

    /// <summary>   Gets insert statement.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The insert statement.</returns>
    public virtual string GetInsertStatement(Type type)
    {
        var columnList = RenderColumnList(type).ToString();
        var parameterList = RenderParameterList(type).ToString();

        return
            $"INSERT INTO {RenderTableName(type)} ({columnList}) VALUES ({parameterList})";
    }

    /// <summary>	Gets insert automatic key statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The insert automatic key statement. </returns>
    public virtual string GetInsertAutoKeyStatement(Type type)
    {
        var key = SqlCache.TypeKeyPropertiesCache(type).Single(tk => tk.ExtendedData.IdentityKey).PropertyInfo;
        var columnList = RenderAutoKeyColumnList(type).ToString();
        var parameterList = RenderAutoKeyParameterList(type).ToString();

        return
            $"INSERT INTO {RenderTableName(type)} ({columnList}) VALUES ({parameterList}){GetAutoKeyStatement(key)}";
    }

    /// <summary>   Gets insert multiple statement.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The insert multiple statement.</returns>
    public string GetInsertMultipleStatement(Type type)
    {
        var columnList = RenderAutoKeyColumnList(type).ToString();
        var parameterList = RenderAutoKeyParameterList(type).ToString();

        return $"INSERT INTO {RenderTableName(type)} ({columnList}) VALUES ({parameterList})";
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
    public abstract string GetAutoKeyStatement(ImmediateProperty propertyInfo);

    /// <summary>	Gets update statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The update statement. </returns>
    public virtual string GetUpdateStatement(Type type)
    {
        var keys = SqlCache.TypeKeyPropertiesCache(type).ToArray();
        var sb = new StringBuilder();
        for (var i = 0; i < keys.Length; i++)
        {
            if (i > 0)
                sb.Append(" AND ");
            sb.Append($"{RenderPropertyName(keys[i].PropertyInfo)} = {RenderParameterProperty(keys[i].PropertyInfo)}");
        }

        var filterSql = sb.ToString();

        var setClauses = RenderSetStatements(type).ToString();
        return
            $"UPDATE {RenderTableName(type)} " +
            $"SET {setClauses} " +
            $"WHERE {filterSql}";
    }

    /// <summary>   Gets update statement. </summary>
    /// <param name="type">                 The type. </param>
    /// <param name="timestamp">            The timestamp. </param>
    /// <param name="timestampFieldname">   The timestamp fieldname. </param>
    /// <returns>   The update statement. </returns>
    public virtual string GetUpdateStatement(Type type, DateTimeOffset timestamp, string timestampFieldname)
    {
        var keys = SqlCache.TypeKeyPropertiesCache(type).ToArray();
        var sb = new StringBuilder();
        for (var i = 0; i < keys.Length; i++)
        {
            if (i > 0)
                sb.Append(" AND ");
            sb.Append($"{RenderPropertyName(keys[i].PropertyInfo)} = {RenderParameterProperty(keys[i].PropertyInfo)}");
        }

        var filterSql = sb.ToString();

        var setClauses = RenderSetStatements(type).ToString();
        return
            $"UPDATE {RenderTableName(type)} " +
            $"SET {setClauses} " +
            $"WHERE {filterSql} AND {RenderPropertyName(timestampFieldname)} = {RenderParameterProperty("OriginalTimeStamp")}";
    }

    /// <summary>	Gets delete statememt. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The delete statememt. </returns>
    public virtual string GetDeleteStatememt(Type type)
    {
        var keys = SqlCache.TypeKeyPropertiesCache(type).ToArray();
        var sb = new StringBuilder();
        for (var i = 0; i < keys.Length; i++)
        {
            if (i > 0)
                sb.Append(" AND ");
            sb.Append($"{RenderPropertyName(keys[i].PropertyInfo)} = {RenderParameterProperty(keys[i].PropertyInfo)}");
        }

        var filterSql = sb.ToString();
        return
            $"DELETE FROM {RenderTableName(type)} WHERE {filterSql}";
    }

    /// <summary>   Gets delete by statement.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <returns>   The delete by statement.</returns>
    public virtual string GetDeleteByStatememt(Type type, string filterProperty)
    {
        return
            $"DELETE FROM {RenderTableName(type)} WHERE {RenderPropertyName(filterProperty)} = {RenderParameterPropertyName(filterProperty)}";
    }

    #endregion

    #region Rendering

    /// <summary>	Renders the property list described by properties. </summary>
    /// <param name="properties">	The properties. </param>
    /// <returns>	A string. </returns>
    public virtual StringBuilder RenderPropertyList(ImmediateProperty[] properties)
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

    /// <summary>   Renders the property list described by properties.</summary>
    /// <param name="tableType">    Type of the table. </param>
    /// <param name="properties">   The properties. </param>
    /// <returns>   A StringBuilder.</returns>
    public virtual StringBuilder RenderPropertyList(Type tableType, ImmediateProperty[] properties)
    {
        RenderTableName("");
        var sb = new StringBuilder();
        for (var i = 0; i < properties.Length; i++)
        {
            if (i > 0)
                sb.Append(", ");
            sb.Append($"{RenderTableName(tableType)}.{RenderPropertyName(properties[i])}");
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
        // try to find name in cache and return it
        if (SqlCache.EntityNameCache.TryGetValue(type.TypeHandle, out var name)) return RenderTableName(name);

        // generate name
        var newName = EntityNameService.Name(type);

        // add name to cache
        SqlCache.EntityNameCache.TryAdd(type.TypeHandle, newName);

        // return rendered name
        return RenderTableName(newName);
    }

    /// <summary>	Renders the property name described by propertyInfo. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	A string. </returns>
    public virtual string RenderPropertyName(ImmediateProperty propertyInfo)
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

    /// <summary>
    ///     Gets name service.
    /// </summary>
    /// <returns>
    ///     The name service.
    /// </returns>
    public IEntityNameService GetNameService()
    {
        return EntityNameService;
    }
    
    /// <summary>	Renders the parameter property described by propertyInfo. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	A string. </returns>
    public virtual string RenderParameterProperty(ImmediateProperty propertyInfo)
    {
        return RenderParameterProperty(propertyInfo.Name);
    }

    public virtual string RenderParameterProperty(string propertyName)
    {
        return $"@{propertyName}";
    }

    /// <summary>   Renders the parameter property name described by propertyName.</summary>
    /// <param name="propertyName"> Name of the property. </param>
    /// <returns>   A string.</returns>
    public virtual string RenderParameterPropertyName(string propertyName)
    {
        return $"@{propertyName}";
    }

    /// <summary>   Renders the in filter by property described by propertyInfo.</summary>
    /// <param name="propertyInfo">     Information describing the property. </param>
    /// <param name="collectionName">   Name of the collection. </param>
    /// <returns>   A string.</returns>
    public virtual string RenderInFilterByProperty(ImmediateProperty propertyInfo, string collectionName)
    {
        return $"{RenderPropertyName(propertyInfo)} IN @{collectionName}";
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

    /// <summary>   Renders the column list described by type.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   A StringBuilder.</returns>
    public virtual StringBuilder RenderColumnList(Type type)
    {
        var props = SqlCache.TypePropertiesChache(type).ToArray();
        return RenderPropertyList(props);
    }

    /// <summary>   Renders the parameter list described by type.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   A StringBuilder.</returns>
    public virtual StringBuilder RenderParameterList(Type type)
    {
        var sb = new StringBuilder();
        var props = SqlCache.TypePropertiesChache(type).ToArray();
        for (var i = 0; i < props.Length; i++)
        {
            if (i > 0)
                sb.Append(", ");
            sb.Append(RenderParameterProperty(props[i]));
        }

        return sb;
    }

    /// <summary>
    ///     Renders the parameter list described by type.
    /// </summary>
    /// <param name="props">    The properties. </param>
    /// <returns>
    ///     A StringBuilder.
    /// </returns>
    public virtual StringBuilder RenderParameterList(ImmediateProperty[] props)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < props.Length; i++)
        {
            if (i > 0)
                sb.Append(", ");
            sb.Append(RenderParameterProperty(props[i]));
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
    public virtual IEnumerable<ImmediateProperty> GetPropertiesWithoutKey(Type type)
    {
        var props = SqlCache.TypePropertiesChache(type);
        var keyProps = SqlCache.TypeKeyPropertiesCache(type).Select(p => p.PropertyInfo).ToList();

        return keyProps.Count > 1 ? props : props.Except(keyProps);
    }

    #endregion
}