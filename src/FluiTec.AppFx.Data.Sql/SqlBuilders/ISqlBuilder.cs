using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
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
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="propertyNameService">  The property name service. </param>
    protected SqlBuilder(SqlType sqlType, ISqlKeywords keywords, IEntityNameService entityNameService, IPropertyNameService propertyNameService)
    {
        SqlType = sqlType;
        Keywords = keywords;
        EntityNameService = entityNameService;
        PropertyNameService = propertyNameService;
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

    /// <summary>   Gets the entity name service. </summary>
    /// <value> The entity name service. </value>
    public IEntityNameService EntityNameService { get; }

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    public IPropertyNameService PropertyNameService { get; }

    /// <summary>   Wrap expression. </summary>
    /// <param name="expression">   The expression. </param>
    /// <returns>   A string. </returns>
    public abstract string WrapExpression(string expression);

    /// <summary>   Renders the table name described by typeSchema. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderTableName(ITypeSchema typeSchema)
    {
        // TODO : use typeschema!!!
        var name = EntityNameService.GetName(typeSchema.Type);
        return SupportsSchema && name.Schema != null
            ? $"{WrapExpression(name.Schema)}.{WrapExpression(name.Name)}"
            : WrapExpression(name.Name);
    }

    /// <summary>   Renders the property described by property. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   A string. </returns>
    public virtual string RenderProperty(IPropertySchema property)
    {
        // TODO : use propertyschema!!!
        var name = PropertyNameService.GetName(property.PropertyType);
        return WrapExpression(name.Name);
    }
}

/// <summary>   A microsoft SQL builder. </summary>
public class MicrosoftSqlBuilder : SqlBuilder
{
    /// <summary>   Constructor. </summary>
    /// <param name="sqlType">              Type of the SQL. </param>
    /// <param name="keywords">             The keywords. </param>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="propertyNameService">  The property name service. </param>
    public MicrosoftSqlBuilder(SqlType sqlType, ISqlKeywords keywords, IEntityNameService entityNameService, IPropertyNameService propertyNameService) 
        : base(sqlType, keywords, entityNameService, propertyNameService)
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