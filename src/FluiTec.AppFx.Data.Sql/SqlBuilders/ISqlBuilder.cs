using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;
using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   Interface for SQL builder. </summary>
public interface ISqlBuilder
{
    /// <summary> Event queue for all listeners interested in SqlBuilt events.</summary>
    event EventHandler<SqlBuiltEventArgs>? SqlBuilt;

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    SqlType SqlType { get; }

    /// <summary>   Gets the keywords. </summary>
    /// <value> The keywords. </value>
    ISqlKeywords Keywords { get; }

    /// <summary>   Gets a value indicating whether the supports schemata. </summary>
    /// <value> True if supports schemata, false if not. </value>
    bool SupportsSchemata { get; }

    /// <summary>   Renders the table name described by typeSchema. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   A string. </returns>
    string RenderTableName(ITypeSchema typeSchema);

    /// <summary>   Renders the property described by property. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   A string. </returns>
    string RenderProperty(IPropertySchema property);

    /// <summary>   Renders the property assignment. </summary>
    /// <param name="property">             The property. </param>
    /// <param name="assignmentOperator">   The assignment operator. </param>
    /// <param name="parameterName">        (Optional) Name of the parameter. </param>
    /// <returns>   A string. </returns>
    string RenderPropertyAssignment(IPropertySchema property, string assignmentOperator, string? parameterName = null);

    /// <summary>   Renders the property parameter comparison. </summary>
    /// <param name="property">             The property. </param>
    /// <param name="comparisonOperator">   The comparison operator. </param>
    /// <param name="parameterName">        (Optional) Name of the parameter. </param>
    /// <returns>   A string. </returns>
    string RenderPropertyParameterComparison(IPropertySchema property, string comparisonOperator, string? parameterName = null);

    /// <summary>   Renders the join expressions. </summary>
    /// <param name="expressions">      The expressions. </param>
    /// <param name="joinExpression">   The join expression. </param>
    /// <returns>   A string. </returns>
    string RenderJoinExpressions(IEnumerable<string> expressions, string joinExpression);

    /// <summary>   Renders the list described by expressions. </summary>
    /// <param name="expressions">  The expressions. </param>
    /// <returns>   A string. </returns>
    string RenderList(IEnumerable<string> expressions);

    /// <summary>   Creates parameter name. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   The new parameter name. </returns>
    string CreateParameterName(IPropertySchema property);

    /// <summary> Renders the parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    string RenderParameter(string parameterName);

    /// <summary> Renders the offset parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    string RenderOffsetParameter(string parameterName);

    /// <summary> Renders the fetch next parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    string RenderFetchNextParameter(string parameterName);
}