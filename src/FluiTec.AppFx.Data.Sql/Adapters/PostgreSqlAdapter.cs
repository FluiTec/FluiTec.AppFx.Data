using System.Reflection;
using System.Text;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Data.Sql.Adapters;

/// <summary>	A postgre SQL adapter. </summary>
public class PostgreSqlAdapter : SqlAdapter
{
    /// <summary>	Constructor. </summary>
    /// <param name="entityNameService">	The entity name service. </param>
    public PostgreSqlAdapter(IEntityNameService entityNameService) : base(entityNameService)
    {
    }

    /// <summary>   Gets a value indicating whether the supports date time offset. </summary>
    /// <value> True if supports date time offset, false if not. </value>
    public override bool SupportsDateTimeOffset => true;

    /// <summary>	Gets automatic key statement. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	The automatic key statement. </returns>
    public override string GetAutoKeyStatement(PropertyInfo propertyInfo)
    {
        return $" RETURNING {RenderPropertyName(propertyInfo)}";
    }

    /// <summary>	Renders the table name described by tableName. </summary>
    /// <param name="tableName">	Name of the table. </param>
    /// <returns>	A string. </returns>
    public override string RenderTableName(string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            return base.RenderTableName(tableName);
        if (!tableName.Contains("."))
            return $"\"public\".\"{tableName}\"";
        var sb = new StringBuilder();
        var split = tableName.Split('.');
        for (var i = 0; i < split.Length; i++)
        {
            if (i != 0)
                sb.Append('.');
            sb.Append($"\"{split[i]}\"");
        }

        return sb.ToString();
    }

    /// <summary>	Renders the property name described by propertyInfo. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	A string. </returns>
    public override string RenderPropertyName(PropertyInfo propertyInfo)
    {
        return RenderPropertyName(propertyInfo.Name);
    }

    /// <summary>Renders the property name described by propertyName.</summary>
    /// <param name="propertyName"> Name of the property. </param>
    /// <returns>A string.</returns>
    public override string RenderPropertyName(string propertyName)
    {
        return $"\"{propertyName}\"";
    }

    /// <summary>   Renders the in filter by property described by propertyInfo.</summary>
    /// <param name="propertyInfo">     Information describing the property. </param>
    /// <param name="collectionName">   Name of the collection. </param>
    /// <returns>   A string.</returns>
    public override string RenderInFilterByProperty(PropertyInfo propertyInfo, string collectionName)
    {
        return $"{RenderPropertyName(propertyInfo)} = ANY(@{collectionName})";
    }
}