using FluiTec.AppFx.Data.DataProviders;
using NMemory;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.Providers;

/// <summary>   Interface for in memory data provider. </summary>
public interface INMemoryDataProvider : IDataProvider
{
    /// <summary>   Gets the database. </summary>
    /// <value> The database. </value>
    Database Database { get; }

    /// <summary>   Gets the table. </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <returns>   The table. </returns>
    ITable<TEntity> GetTable<TEntity>() where TEntity : class;
}