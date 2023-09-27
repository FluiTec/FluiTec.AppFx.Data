using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.SqlBuilders.EventArguments;
using FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

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

    /// <summary> Event queue for all listeners interested in SqlBuilt events.</summary>
    public event EventHandler<SqlBuiltEventArgs>? SqlBuilt;

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
        var sql = SupportsSchemata && typeSchema.Name.Schema != null
            ? $"{WrapExpression(typeSchema.Name.Schema)}.{WrapExpression(typeSchema.Name.Name)}"
            : WrapExpression(typeSchema.Name.Name);
        OnTypeSqlBuilt(sql, typeSchema);
        return sql;
    }

    /// <summary>   Renders the property described by property. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderProperty(IPropertySchema property)
    {
        var sql = WrapExpression(property.Name.ColumnName);
        OnTypeSqlBuilt(sql, property);
        return sql;
    }

    /// <summary>   Renders the property assignment. </summary>
    /// <param name="property">             The property. </param>
    /// <param name="assignmentOperator">   The assignment operator. </param>
    /// <param name="parameterName">        (Optional) Name of the parameter. </param>
    /// <returns>   A string. </returns>
    public string RenderPropertyAssignment(IPropertySchema property, string assignmentOperator,
        string? parameterName = null)
    {
        var paramName = parameterName ?? CreateParameterName(property);
        var sql = $"{WrapExpression(property.Name.ColumnName)} {assignmentOperator} {RenderParameter(paramName)}";
        OnTypeSqlBuilt(sql, property);
        return sql;
    }

    /// <summary>   Renders the property parameter comparison. </summary>
    /// <param name="property">             The property. </param>
    /// <param name="comparisonOperator">   The comparison operator. </param>
    /// <param name="parameterName">        (Optional) Name of the parameter. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderPropertyParameterComparison(IPropertySchema property, string comparisonOperator,
        string? parameterName = null)
    {
        var paramName = parameterName ?? CreateParameterName(property);
        var sql = $"{WrapExpression(property.Name.ColumnName)} {comparisonOperator} {RenderParameter(paramName)}";
        OnTypeSqlBuilt(sql, property);
        return sql;
    }

    /// <summary>   Renders the join expressions. </summary>
    /// <param name="expressions">      The expressions. </param>
    /// <param name="joinExpression">   The join expression. </param>
    /// <returns>   A string. </returns>
    public string RenderJoinExpressions(IEnumerable<string> expressions, string joinExpression)
    {
        return string.Join($"{joinExpression} ", expressions);
    }

    /// <summary>   Renders the list described by expressions. </summary>
    /// <param name="expressions">  The expressions. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderList(IEnumerable<string> expressions)
    {
        return RenderJoinExpressions(expressions, Keywords.ListSeparator);
    }

    /// <summary>   Creates parameter name. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   The new parameter name. </returns>
    public virtual string CreateParameterName(IPropertySchema property)
    {
        return $"{property.Name.Name}";
    }

    /// <summary> Renders the parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    public virtual string RenderParameter(string parameterName)
    {
        return $"@{parameterName}";
    }

    /// <summary> Renders the offset parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    public string RenderOffsetParameter(string parameterName)
    {
        return $"{Keywords.OffsetExpression} {RenderParameter(parameterName)} {Keywords.OffsetRowsExpression}";
    }

    /// <summary> Renders the fetch next parameter described by parameterName.</summary>
    /// <param name="parameterName"> Name of the parameter. </param>
    /// <returns> A string.</returns>
    public string RenderFetchNextParameter(string parameterName)
    {
        return $"{Keywords.FetchNextExpressions} {RenderParameter(parameterName)} {Keywords.FetchNextRowsExpression}";
    }

    /// <summary> Executes the 'type SQL built' action.</summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="sql">      The SQL. </param>
    /// <param name="schema">   The schema. </param>
    /// <param name="renderer"> (Optional) The renderer. </param>
    protected virtual void OnTypeSqlBuilt(string sql, ITypeSchema schema, [CallerMemberName] string? renderer = null)
    {
        if (renderer == null)
            throw new ArgumentNullException(nameof(renderer));
        SqlBuilt?.Invoke(this, new TypeSqlBuiltEventArgs(sql, renderer, schema));
    }

    /// <summary> Executes the 'type SQL built' action.</summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="sql">      The SQL. </param>
    /// <param name="schema">   The schema. </param>
    /// <param name="renderer"> (Optional) The renderer. </param>
    protected virtual void OnTypeSqlBuilt(string sql, IPropertySchema schema,
        [CallerMemberName] string? renderer = null)
    {
        if (renderer == null)
            throw new ArgumentNullException(nameof(renderer));
        SqlBuilt?.Invoke(this, new PropertySqlBuiltEventArgs(sql, renderer, schema));
    }

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public abstract string WrapExpression(string expression);
}