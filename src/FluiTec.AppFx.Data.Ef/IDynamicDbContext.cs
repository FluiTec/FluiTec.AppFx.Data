using System;
using FluiTec.AppFx.Data.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
/// Interface for dynamic database context.
/// </summary>
public interface IDynamicDbContext : IDisposable
{
    /// <summary>
    /// Gets the type of the SQL.
    /// </summary>
    ///
    /// <value>
    /// The type of the SQL.
    /// </value>
    SqlType SqlType { get; }

    /// <summary>
    /// Gets the connection string.
    /// </summary>
    ///
    /// <value>
    /// The connection string.
    /// </value>
    string ConnectionString { get; }

    /// <summary>
    /// Gets the set.
    /// </summary>
    ///
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    ///
    /// <returns>
    /// A DbSet&lt;TEntity&gt;
    /// </returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    /// <summary>
    /// Saves the changes.
    /// </summary>
    ///
    /// <returns>
    /// An int.
    /// </returns>
    int SaveChanges();

    /// <summary>
    /// Entries the given entity.
    /// </summary>
    ///
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <param name="entity">   The entity. </param>
    ///
    /// <returns>
    /// An EntityEntry&lt;TEntity&gt;
    /// </returns>
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}