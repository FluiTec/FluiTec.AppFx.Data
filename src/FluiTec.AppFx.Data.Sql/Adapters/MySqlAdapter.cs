using FluiTec.AppFx.Data.EntityNameServices;
using ImmediateReflection;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Sql.Adapters;

/// <summary>	a MySql adapter. </summary>
public class MySqlAdapter : SqlAdapter
{
    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="logger">               The logger. </param>
    public MySqlAdapter(IEntityNameService entityNameService, ILogger<ISqlAdapter> logger) : base(entityNameService, logger)
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
        return $";SELECT LAST_INSERT_ID() {RenderPropertyName(propertyInfo)}";
    }
}