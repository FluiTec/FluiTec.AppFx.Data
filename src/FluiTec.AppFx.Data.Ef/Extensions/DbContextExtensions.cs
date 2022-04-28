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
        var (item1, item2) = nameService.SchemaAndName(typeof(TEntity));
        return (EntityTypeBuilder<TEntity>) ((EntityTypeBuilder) entityTypeBuilder).ToTable(item2, item1);
    }

    /// <summary>
    /// An EntityTypeBuilder&lt;TEntity&gt; extension method that converts this object to a table.
    /// </summary>
    ///
    /// <param name="entityTypeBuilder">    The entityTypeBuilder to act on. </param>
    /// <param name="nameService">          The name service. </param>
    ///
    /// <returns>
    /// The given data converted to an EntityTypeBuilder&lt;TEntity&gt;
    /// </returns>
    public static EntityTypeBuilder ToTable(this EntityTypeBuilder entityTypeBuilder, IEntityNameService nameService)
    {
        var (item1, item2) = nameService.SchemaAndName(entityTypeBuilder.Metadata.ClrType);
        return entityTypeBuilder.ToTable(item2, item1);
    }
}