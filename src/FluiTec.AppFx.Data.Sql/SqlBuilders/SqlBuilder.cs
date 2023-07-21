using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Enums;

namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   A SQL builder. </summary>
public abstract class SqlBuilder : ISqlBuilder
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="sqlType">              Type of the SQL. </param>
    /// <param name="keywords">             The keywords. </param>
    protected SqlBuilder(SqlType sqlType, ISqlKeywords keywords)
    {
        SqlType = sqlType;
        Keywords = keywords;
    }

    /// <summary>   Gets a value indicating whether the supports schemata. </summary>
    /// <value> True if supports schemata, false if not. </value>
    public abstract bool SupportsSchemata { get; }

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public SqlType SqlType { get; }

    /// <summary>   Gets the keywords. </summary>
    /// <value> The keywords. </value>
    public ISqlKeywords Keywords { get; }

    /// <summary>   Renders the table name described by typeSchema. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderTableName(ITypeSchema typeSchema)
    {
        return SupportsSchemata && typeSchema.Name.Schema != null
            ? $"{WrapExpression(typeSchema.Name.Schema)}.{WrapExpression(typeSchema.Name.Name)}"
            : WrapExpression(typeSchema.Name.Name);
    }

    /// <summary>   Renders the property described by property. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderProperty(IPropertySchema property)
    {
        return WrapExpression(property.Name.ColumnName);
    }

    /// <summary>   Renders the list described by expressions. </summary>
    /// <param name="expressions">  The expressions. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderList(IEnumerable<string> expressions)
    {
        return string.Join($"{Keywords.ListSeparator} ", expressions);
    }

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public abstract string WrapExpression(string expression);
}