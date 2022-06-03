using System;
using System.Linq;
using FluiTec.AppFx.Data.EntityNameServices;
using ImmediateReflection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Sql.Adapters;

/// <summary>   A sq lite adapter. </summary>
public class SqLiteAdapter : SqlAdapter
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="logger">               The logger. </param>
    public SqLiteAdapter(IEntityNameService entityNameService, ILogger<ISqlAdapter> logger) : base(entityNameService,
        logger)
    {
    }

    /// <summary>   Gets a value indicating whether the supports date time offset. </summary>
    /// <value> True if supports date time offset, false if not. </value>
    public override bool SupportsDateTimeOffset => false;

    /// <summary>	Renders the table name described by tableName. </summary>
    /// <param name="tableName">	Name of the table. </param>
    /// <returns>	A string. </returns>
    public override string RenderTableName(string tableName)
    {
        return string.IsNullOrWhiteSpace(tableName)
            ? base.RenderTableName(tableName)
            : tableName.Replace('.', '_');
    }

    /// <summary>	Gets automatic key statement. </summary>
    /// <param name="propertyInfo">	Information describing the property. </param>
    /// <returns>	The automatic key statement. </returns>
    public override string GetAutoKeyStatement(ImmediateProperty propertyInfo)
    {
        return $";SELECT last_insert_rowid() {RenderPropertyName(propertyInfo)}";
    }

    /// <summary>
    /// Select paged statement.
    /// </summary>
    ///
    /// <param name="type">                         The type. </param>
    /// <param name="skipRecordCountParameterName"> Name of the skip record count parameter. </param>
    /// <param name="takeRecordCountParameterName"> Name of the take record count parameter. </param>
    ///
    /// <returns>
    /// A string.
    /// </returns>
    public override string SelectPagedStatement(Type type, string skipRecordCountParameterName, string takeRecordCountParameterName)
    {
        return $"SELECT {RenderPropertyList(SqlCache.TypePropertiesChache(type).ToArray())} " +
               $"FROM {RenderTableName(type)} " +
               $"ORDER BY {RenderPropertyName(SqlCache.TypeKeyPropertiesCache(type).First().PropertyInfo)} " +
               $"LIMIT {RenderParameterPropertyName(skipRecordCountParameterName)},{RenderParameterPropertyName(takeRecordCountParameterName)}";
    }

    /// <summary>
    /// Page ordered statement.
    /// </summary>
    ///
    /// <param name="statement">                    The statement. </param>
    /// <param name="skipRecordCountParameterName"> Name of the skip record count parameter. </param>
    /// <param name="takeRecordCountParameterName"> Name of the take record count parameter. </param>
    ///
    /// <returns>
    /// A string.
    /// </returns>
    public override string PageOrderedStatement(string statement, string skipRecordCountParameterName, string takeRecordCountParameterName)
    {
        return $"{statement} " +
               $"LIMIT {RenderParameterPropertyName(skipRecordCountParameterName)},{RenderParameterPropertyName(takeRecordCountParameterName)}";
    }
}