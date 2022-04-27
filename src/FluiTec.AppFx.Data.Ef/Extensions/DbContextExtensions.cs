using System;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluiTec.AppFx.Data.Ef.Extensions;

/// <summary>
/// A database context extensions.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Gets an option.
    /// </summary>
    ///
    /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
    ///                                                 the required range. </exception>
    ///
    /// <param name="sqlType">          Type of the SQL. </param>
    /// <param name="connectionString"> The connection string. </param>
    ///
    /// <returns>
    /// The option.
    /// </returns>
    public static DbContextOptions GetOption(SqlType sqlType, string connectionString)
    {
        return sqlType switch
        {
            SqlType.Mssql => new DbContextOptionsBuilder().UseSqlServer(connectionString).Options,
            SqlType.Mysql => new DbContextOptionsBuilder().UseMySQL(connectionString).Options,
            SqlType.Pgsql => new DbContextOptionsBuilder().UseNpgsql(connectionString).Options,
            SqlType.Sqlite => new DbContextOptionsBuilder().UseSqlite(connectionString).Options,
            _ => throw new ArgumentOutOfRangeException(nameof(sqlType), sqlType, null)
        };
    }

    /// <summary>
    /// An EntityTypeBuilder&lt;TEntity&gt; extension method that converts this object to a table.
    /// </summary>
    ///
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <param name="entityTypeBuilder">    The entityTypeBuilder to act on. </param>
    /// <param name="nameService">          The name service. </param>
    ///
    /// <returns>
    /// The given data converted to an EntityTypeBuilder&lt;TEntity&gt;
    /// </returns>
    public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder,
        IEntityNameService nameService) where TEntity : class
    {
        var schemaAndName = nameService.SchemaAndName(typeof(TEntity));
        return (EntityTypeBuilder<TEntity>) ((EntityTypeBuilder) entityTypeBuilder).ToTable(schemaAndName.Item2, schemaAndName.Item1);
    }
}