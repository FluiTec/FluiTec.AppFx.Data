﻿using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Enums;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   Interface for SQL builder. </summary>
public interface ISqlBuilder
{
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

    /// <summary>   Renders the list described by expressions. </summary>
    /// <param name="expressions">  The expressions. </param>
    /// <returns>   A string. </returns>
    string RenderList(IEnumerable<string> expressions);
}