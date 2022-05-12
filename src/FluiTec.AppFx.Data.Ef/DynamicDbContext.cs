using System.Diagnostics;
using System.Linq;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.Ef.Extensions;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Sql;
using Microsoft.EntityFrameworkCore;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
///     A dynamic database context.
/// </summary>
public class DynamicDbContext : DbContext, IDynamicDbContext
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    public DynamicDbContext(IEfDataService dataService) : base(
        DbContextExtensions.GetOption(dataService.SqlType, dataService.ConnectionString))
    {
        SqlType = dataService.SqlType;
        ConnectionString = dataService.ConnectionString;
        NameService = dataService.NameService;
    }

    /// <summary>
    ///     Gets the name service.
    /// </summary>
    /// <value>
    ///     The name service.
    /// </value>
    public IEntityNameService NameService { get; }

    /// <summary>
    ///     Gets the type of the SQL.
    /// </summary>
    /// <value>
    ///     The type of the SQL.
    /// </value>
    public SqlType SqlType { get; }

    /// <summary>
    ///     Gets the connection string.
    /// </summary>
    /// <value>
    ///     The connection string.
    /// </value>
    public string ConnectionString { get; }

    /// <summary>
    ///     <para>
    ///         Override this method to configure the database (and other options) to be used
    ///         for this context. This method is called for each instance of the context that
    ///         is created. The base implementation does nothing.
    ///     </para>
    ///     <para>
    ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" />
    ///         may or may not have been passed to the constructor, you can use
    ///         <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" />
    ///         to determine if the options have already been set, and skip some or all of
    ///         the logic in
    ///         <see
    ///             cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />
    ///         .
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     See
    ///     <see href="https://aka.ms/efcore-docs-dbcontext">
    ///         DbContext lifetime, configuration, and
    ///         initialization
    ///     </see>
    ///     for more information.
    /// </remarks>
    /// <param name="optionsBuilder">
    ///     A builder used to create or modify options for this context.
    ///     Databases (and other extensions)
    ///     typically define extension methods on this object that allow
    ///     you to configure the context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(s => Debug.WriteLine(s));
    }

    /// <summary>
    ///     Configure model for entity.
    /// </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <param name="modelBuilder"> The model builder. </param>
    protected void ConfigureModelForEntity<TEntity>(ModelBuilder modelBuilder)
        where TEntity : class, IEntity
    {
        var entityType = typeof(TEntity);

        // configure schema and name of table
        modelBuilder.Entity(entityType)
            .ToTable(NameService, SqlType);

        // configure keys
        var keys = SqlCache.TypeKeyPropertiesCache(entityType).ToArray();
        if (keys.Any())
            modelBuilder.Entity(entityType)
                .HasKey(keys.Select(p => p.PropertyInfo.Name).ToArray());

        var props = SqlCache.TypePropertiesChache(entityType).ToArray();
        foreach (var prop in props)
        {
            var propBuilder = modelBuilder.Entity(entityType)
                .Property(prop.Name);
            if (prop.Equals(keys.Single().PropertyInfo) && keys.Single().ExtendedData.IdentityKey)
                propBuilder.UseIdentityColumn();
        }
    }
}