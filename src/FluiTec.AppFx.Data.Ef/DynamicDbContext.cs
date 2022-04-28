using System;
using System.Linq;
using FluiTec.AppFx.Data.Ef.Extensions;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
/// A dynamic database context.
/// </summary>
public class DynamicDbContext : DbContext, IDynamicDbContext
{
    private readonly Type[] _supportedIdentityTypes = { typeof(int), typeof(long) };

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
    /// Gets the name service.
    /// </summary>
    ///
    /// <value>
    /// The name service.
    /// </value>
    public IEntityNameService NameService { get; }

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
        NameService = SqlBuilder.Adapter.GetNameService();
    }

    /// <summary>
    /// Configure model for entity.
    /// </summary>
    ///
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <param name="modelBuilder"> The model builder. </param>
    protected void ConfigureModelForEntity<TEntity>(ModelBuilder modelBuilder) 
        where TEntity : class, IEntity
    {
        var entityType = typeof(TEntity);

        // configure schema and name of table
        modelBuilder.Entity(entityType)
            .ToTable(NameService);

        // configure keys
        var keys = SqlCache.TypeKeyPropertiesCache(entityType).ToArray();
        if (keys.Any())
        {
            modelBuilder.Entity(entityType)
                .HasKey(keys.Select(p => p.PropertyInfo.Name).ToArray());
        }

        var props = SqlCache.TypePropertiesChache(entityType).ToArray();
        foreach (var prop in props)
        {
            var propBuilder = modelBuilder.Entity(entityType)
                .Property(prop.Name);
            if (keys.Length == 1 && prop == keys.Single().PropertyInfo && _supportedIdentityTypes.Contains(prop.PropertyType))
                propBuilder.UseIdentityColumn();
        }
    }
}