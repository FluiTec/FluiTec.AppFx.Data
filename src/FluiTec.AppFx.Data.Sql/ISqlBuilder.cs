using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Sql.Adapters;
using FluiTec.AppFx.Data.Sql.EventArgs;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.Sql;

/// <summary>
///     Interface for SQL builder.
/// </summary>
public interface ISqlBuilder
{
    /// <summary>	The adapter. </summary>
    ISqlAdapter Adapter { get; }

    /// <summary>
    ///     Event queue for all listeners interested in SqlGenerated events.
    /// </summary>
    public event EventHandler<SqlGeneratedEventArgs> SqlGenerated;

    /// <summary>	Select all. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string SelectAll(Type type);

    /// <summary>	Select by key. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string SelectByKey(Type type);

    /// <summary>	Select by filter. </summary>
    /// <param name="type">			 	The type. </param>
    /// <param name="filterProperty">	The filter property. </param>
    /// <param name="selectFields">  	(Optional) The select fields. </param>
    /// <returns>	A string. </returns>
    string SelectByFilter(Type type, string filterProperty, string[] selectFields = null);

    /// <summary>	Select by filter. </summary>
    /// <param name="type">			   	The type. </param>
    /// <param name="filterProperties">	The filter properties. </param>
    /// <param name="selectFields">	   	(Optional) The select fields. </param>
    /// <returns>	A string. </returns>
    string SelectByFilter(Type type, string[] filterProperties, string[] selectFields = null);

    /// <summary>   Select by in filter.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <param name="collectionName">   Name of the collection. </param>
    /// <param name="selectFields">     (Optional) The select fields. </param>
    /// <returns>   A string.</returns>
    string SelectByInFilter(Type type, string filterProperty, string collectionName,
        string[] selectFields = null);

    /// <summary>   Inserts the given type.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   A string.</returns>
    string Insert(Type type);

    /// <summary>	Inserts an automatic key described by type. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string InsertAutoKey(Type type);

    /// <summary>   Inserts a multiple described by type.</summary>
    /// <param name="type"> The type. </param>
    /// <returns>   A string.</returns>
    string InsertMultiple(Type type);

    /// <summary>	Inserts an automatic multiple. </summary>
    /// <param name="type"> 	The type. </param>
    /// <returns>	A string. </returns>
    string InsertAutoMultiple(Type type);

    /// <summary>	Updates the given type. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string Update(Type type);

    /// <summary>   Updates the given type. </summary>
    /// <param name="type">                 The type. </param>
    /// <param name="timestamp">            The timestamp. </param>
    /// <param name="timestampFieldname">   The timestamp fieldname. </param>
    /// <returns>   A string. </returns>
    string Update(Type type, DateTimeOffset timestamp, string timestampFieldname);

    /// <summary>	Deletes the given type. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string Delete(Type type);

    /// <summary>   Deletes the by.</summary>
    /// <param name="type">             The type. </param>
    /// <param name="filterProperty">   The filter property. </param>
    /// <returns>   A string.</returns>
    string DeleteBy(Type type, string filterProperty);

    /// <summary>	Key parameter. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A string. </returns>
    string KeyParameter(Type type);

    /// <summary>	Parameter list. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A List&lt;KeyValuePair&lt;PropertyInfo,string&gt;&gt; </returns>
    List<KeyValuePair<ImmediateProperty, string>> ParameterList(Type type);
}