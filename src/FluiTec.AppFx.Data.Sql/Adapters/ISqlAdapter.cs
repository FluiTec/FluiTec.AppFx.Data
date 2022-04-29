using System;
using System.Text;
using ImmediateReflection;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Data.Sql.Adapters;

/// <summary>	Interface for SQL adapter. </summary>
public interface ISqlAdapter
{
    /// <summary>   Gets a value indicating whether the supports date time offset. </summary>
    /// <value> True if supports date time offset, false if not. </value>
    bool SupportsDateTimeOffset { get; }

    /// <summary>	Select all statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string SelectAllStatement(Type type);

    /// <summary>	Gets by key statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The by key statement. </returns>
    string GetByKeyStatement(Type type);

    /// <summary>	Gets key parameter. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The key parameter. </returns>
    string GetKeyParameters(Type type);

    /// <summary>   Gets insert statement.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The insert statement.</returns>
    string GetInsertStatement(Type type);

    /// <summary>	Gets insert automatic key statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The insert automatic key statement. </returns>
    string GetInsertAutoKeyStatement(Type type);

    /// <summary>   Gets insert multiple statement.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The insert multiple statement.</returns>
    string GetInsertMultipleStatement(Type type);

    /// <summary>	Gets insert automatic key multiple statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The insert automatic key multiple statement. </returns>
    string GetInsertAutoKeyMultipleStatement(Type type);

    /// <summary>	Gets update statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The update statement. </returns>
    string GetUpdateStatement(Type type);

    /// <summary>   Gets update statement. </summary>
    /// <param name="type">                 The type. </param>
    /// <param name="timestamp">            The timestamp. </param>
    /// <param name="timestampFieldname">   The timestamp fieldname. </param>
    /// <returns>   The update statement. </returns>
    string GetUpdateStatement(Type type, DateTimeOffset timestamp, string timestampFieldname);

    /// <summary>	Gets delete statement. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	The delete statement. </returns>
    string GetDeleteStatememt(Type type);

    /// <summary>   Gets delete by statement.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <returns>   The delete by statement.</returns>
    string GetDeleteByStatememt(Type type, string filterProperty);

    /// <summary>	Renders the property list described by properties. </summary>
    /// <param name="properties">	The properties. </param>
    /// <returns>	A StringBuilder. </returns>
    StringBuilder RenderPropertyList(ImmediateProperty[] properties);

    /// <summary>   Renders the property list described by properties.</summary>
    /// <param name="tableType">    Type of the table. </param>
    /// <param name="properties">   The properties. </param>
    /// <returns>   A StringBuilder.</returns>
    StringBuilder RenderPropertyList(Type tableType, ImmediateProperty[] properties);

    /// <summary>	Renders the parameter property described by propertyInfo. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	A string. </returns>
    string RenderParameterProperty(ImmediateProperty propertyInfo);

    /// <summary>   Renders the parameter property described by propertyInfo. </summary>
    /// <param name="propertyName"> Name of the property. </param>
    /// <returns>   A string. </returns>
    // ReSharper disable once UnusedMemberInSuper.Global
    string RenderParameterProperty(string propertyName);

    /// <summary>	Renders the table name described by type. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string RenderTableName(Type type);

    /// <summary>	Gets by filter statement. </summary>
    /// <param name="type">			 	The type. </param>
    /// <param name="filterProperty">	The filter property. </param>
    /// <param name="selectFields">  	The select fields. </param>
    /// <returns>	The by filter statement. </returns>
    string GetByFilterStatement(Type type, string filterProperty, string[] selectFields);

    /// <summary>   Gets by filter in statement.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <param name="collectionName">   Name of the collection. </param>
    /// <param name="selectFields">     The select fields. </param>
    /// <returns>   The by filter in statement.</returns>
    string GetByFilterInStatement(Type type, string filterProperty, string collectionName, string[] selectFields);

    /// <summary>	Gets by filter statement. </summary>
    /// <param name="type">			   	The type. </param>
    /// <param name="filterProperties">	The filter properties. </param>
    /// <param name="selectFields">	   	The select fields. </param>
    /// <returns>	The by filter statement. </returns>
    string GetByFilterStatement(Type type, string[] filterProperties, string[] selectFields);

    /// <summary>Renders the property name described by propertyInfo.</summary>
    /// <param name="propertyInfo"> Information describing the property. </param>
    /// <returns>A string.</returns>
    string RenderPropertyName(ImmediateProperty propertyInfo);

    /// <summary>Renders the property name described by propertyName.</summary>
    /// <param name="propertyName"> Name of the property. </param>
    /// <returns>A string.</returns>
    string RenderPropertyName(string propertyName);

    /// <summary>
    ///     Gets name service.
    /// </summary>
    /// <returns>
    ///     The name service.
    /// </returns>
    IEntityNameService GetNameService();
}