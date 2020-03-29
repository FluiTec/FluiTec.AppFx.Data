using System;
using System.Reflection;
using System.Text;

namespace FluiTec.AppFx.Data.Sql.Adapters
{
    /// <summary>	Interface for SQL adapter. </summary>
    public interface ISqlAdapter
    {
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
        string GetKeyParameter(Type type);

        /// <summary>	Gets insert automatic key statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The insert automatic key statement. </returns>
        string GetInsertAutoKeyStatement(Type type);

        /// <summary>	Gets insert automatic key multiple statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The insert automatic key multiple statement. </returns>
        string GetInsertAutoKeyMultipleStatement(Type type);

        /// <summary>	Gets update statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The update statement. </returns>
        string GetUpdateStatement(Type type);

        /// <summary>	Gets delete statement. </summary>
        /// <param name="type">	The type. </param>
        /// <returns>	The delete statement. </returns>
        string GetDeleteStatememt(Type type);

        /// <summary>	Renders the property list described by properties. </summary>
        /// <param name="properties">	The properties. </param>
        /// <returns>	A StringBuilder. </returns>
        StringBuilder RenderPropertyList(PropertyInfo[] properties);

        /// <summary>	Renders the parameter property described by propertyInfo. </summary>
        /// <param name="propertyInfo">	Information describing the property. </param>
        /// <returns>	A string. </returns>
        string RenderParameterProperty(PropertyInfo propertyInfo);

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

        /// <summary>	Gets by filter statement. </summary>
        /// <param name="type">			   	The type. </param>
        /// <param name="filterProperties">	The filter properties. </param>
        /// <param name="selectFields">	   	The select fields. </param>
        /// <returns>	The by filter statement. </returns>
        string GetByFilterStatement(Type type, string[] filterProperties, string[] selectFields);

        /// <summary>Renders the property name described by propertyInfo.</summary>
        /// <param name="propertyInfo"> Information describing the property. </param>
        /// <returns>A string.</returns>
        string RenderPropertyName(PropertyInfo propertyInfo);

        /// <summary>Renders the property name described by propertyName.</summary>
        /// <param name="propertyName"> Name of the property. </param>
        /// <returns>A string.</returns>
        string RenderPropertyName(string propertyName);
    }
}