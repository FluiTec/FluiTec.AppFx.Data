using FluiTec.AppFx.Data.DataServices;
using NMemory;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.DataServices;

/// <summary>
///     Interface for in memory data service.
/// </summary>
public interface INMemoryDataService : IDataService
{
    /// <summary>
    ///     Gets the database.
    /// </summary>
    /// <value>
    ///     The database.
    /// </value>
    Database Database { get; }

    /// <summary>
    ///     Gets the table.
    /// </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <returns>
    ///     The table.
    /// </returns>
    ITable<TEntity> GetTable<TEntity>() where TEntity : class;
}