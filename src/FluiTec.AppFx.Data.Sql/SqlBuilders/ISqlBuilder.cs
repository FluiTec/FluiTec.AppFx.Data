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

    /// <summary>   Renders the table name described by typeSchema. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   A string. </returns>
    string RenderTableName(ITypeSchema typeSchema);
}

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

    /// <summary>   Gets a value indicating whether the supports schema. </summary>
    /// <value> True if supports schema, false if not. </value>
    public abstract bool SupportsSchema { get; }

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    public SqlType SqlType { get; }

    /// <summary>   Gets the keywords. </summary>
    /// <value> The keywords. </value>
    public ISqlKeywords Keywords { get; }

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public abstract string WrapExpression(string expression);

    /// <summary>   Renders the table name described by typeSchema. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderTableName(ITypeSchema typeSchema)
    {
        return SupportsSchema && typeSchema.Name.Schema != null
            ? $"{WrapExpression(typeSchema.Name.Schema)}.{WrapExpression(typeSchema.Name.Name)}"
            : WrapExpression(typeSchema.Name.Name);
    }

    /// <summary>   Renders the property described by property. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderProperty(IPropertySchema property)
    {
        return WrapExpression(property.Name.Name);
    }
}

/// <summary>   A microsoft SQL builder. </summary>
public class MicrosoftSqlBuilder : SqlBuilder
{
    /// <summary>   Constructor. </summary>
    /// <param name="sqlType">              Type of the SQL. </param>
    /// <param name="keywords">             The keywords. </param>
    public MicrosoftSqlBuilder(SqlType sqlType, ISqlKeywords keywords) 
        : base(sqlType, keywords)
    {
    }

    /// <summary>   Gets a value indicating whether the supports schema. </summary>
    /// <value> True if supports schema, false if not. </value>
    public override bool SupportsSchema => true;

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public override string WrapExpression(string expression)
    {
        return $"[{expression}]";
    }
}