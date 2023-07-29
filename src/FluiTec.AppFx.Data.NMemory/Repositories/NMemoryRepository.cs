using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>   A memory repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class NMemoryRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public NMemoryRepository(IDataService dataService, INMemoryDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
        DataProvider = dataProvider;
        UnitOfWork = unitOfWork as NMemoryUnitOfWork ?? throw new InvalidOperationException();
        Table = dataProvider.GetTable<TEntity>();
    }

    public new NMemoryUnitOfWork UnitOfWork { get; }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public new INMemoryDataProvider DataProvider { get; }

    /// <summary>   Gets the table. </summary>
    /// <value> The table. </value>
    public ITable<TEntity> Table { get; protected set; }

    /// <summary>   Gets all items in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public override IEnumerable<TEntity> GetAll()
    {
        return Table.ToList();
    }

    /// <summary>   Gets all asynchronous. </summary>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   all. </returns>
    public override Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetAll());
    }

    /// <summary>   Gets the count. </summary>
    /// <returns>   A long. </returns>
    public override long Count()
    {
        return Table.Count;
    }

    /// <summary>   Count asynchronous. </summary>
    /// <returns>   The count. </returns>
    public override Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Table.Count);
    }
}