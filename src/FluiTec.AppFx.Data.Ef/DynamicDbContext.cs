using System;
using FluiTec.AppFx.Data.Ef.Extensions;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
/// A dynamic database context.
/// </summary>
public class DynamicDbContext : DbContext, IDynamicDbContext
{
    /// <summary>
    /// Gets the type of the SQL.
    /// </summary>
    ///
    /// <value>
    /// The type of the SQL.
    /// </value>
    public SqlType SqlType { get; }

    /// <summary>
    /// Gets the connection string.
    /// </summary>
    ///
    /// <value>
    /// The connection string.
    /// </value>
    public string ConnectionString { get; }

    /// <summary>
    /// Gets the SQL builder.
    /// </summary>
    ///
    /// <value>
    /// The SQL builder.
    /// </value>
    public SqlBuilder SqlBuilder { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="sqlType">          Type of the SQL. </param>
    /// <param name="connectionString"> The connection string. </param>
    public DynamicDbContext(SqlType sqlType, string connectionString) 
        : base(DbContextExtensions.GetOption(sqlType, connectionString))
    {
        SqlType = sqlType;
        ConnectionString = connectionString;
        SqlBuilder = sqlType.GetBuilder();
    }

    /// <summary>
    /// Gets table name.
    /// </summary>
    ///
    /// <param name="type"> The type. </param>
    ///
    /// <returns>
    /// The table name.
    /// </returns>
    protected string GetTableName(Type type)
    {
        var x = SqlBuilder.Adapter.RenderTableName(type);
        return SqlBuilder.Adapter.RenderTableName(type)
            .Replace("]", "", StringComparison.OrdinalIgnoreCase)
            .Replace("[", "", StringComparison.OrdinalIgnoreCase);
    }
}