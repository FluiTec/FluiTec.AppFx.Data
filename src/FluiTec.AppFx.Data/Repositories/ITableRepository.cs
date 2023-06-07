using System.Threading;
using System.Threading.Tasks;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   Interface for table repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface ITableRepository<TEntity> : IPagedRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Gets the name of the table. </summary>
    /// <value> The name of the table. </value>
    string TableName { get; }

    /// <summary>   Gets a t entity using the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   A TEntity. </returns>
    TEntity? Get(params object[] keys);

    /// <summary>   Gets an asynchronous. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>   The asynchronous. </returns>
    Task<TEntity?> GetAsync(object[] keys, CancellationToken cancellationToken = default);
}