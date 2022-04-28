using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>Interface for a table based repository.</summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="FluiTec.AppFx.Data.Repositories.IDataRepository{TEntity}" />
public interface ITableDataRepository<TEntity> : IDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>	Gets the name of the table. </summary>
    /// <value>	The name of the table. </value>
    // ReSharper disable once UnusedMemberInSuper.Global
    string TableName { get; }

    /// <summary>
    ///     Gets an entity using the given identifier.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    TEntity Get(params object[] keys);

    /// <summary>
    ///     Gets an entity asynchronous.
    /// </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     A TEntity.
    /// </returns>
    Task<TEntity> GetAsync(object[] keys, CancellationToken ctx = default);
}