using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>   A memory table repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class NMemoryTableRepository<TEntity> : NMemoryPagedRepository<TEntity>, ITableRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public NMemoryTableRepository(IDataService dataService, INMemoryDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
        KeyProperties = DataService.Schema[EntityType].KeyProperties
            .OrderBy(p => p.Order)
            .ToList();
    }

    /// <summary>   Gets the key properties. </summary>
    /// <value> The key properties. </value>
    public IList<IKeyPropertySchema> KeyProperties { get; }

    /// <summary>   Gets a t entity using the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   A TEntity. </returns>
    public TEntity? Get(params object[] keys)
    {
        return Table.SingleOrDefault(entity => KeysMatch(entity, keys));
    }

    /// <summary>   Gets an asynchronous. </summary>
    /// <param name="keys">                 A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   The asynchronous. </returns>
    public Task<TEntity?> GetAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Get(keys));
    }

    /// <summary>   Keys match. </summary>
    /// <param name="entity">   The entity. </param>
    /// <param name="keys">     A variable-length parameters list containing keys. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    protected bool KeysMatch(TEntity entity, IReadOnlyList<object> keys)
    {
        return !KeyProperties.Where((t, i) => !t.GetValue(entity).Equals(keys[i])).Any();
    }
}