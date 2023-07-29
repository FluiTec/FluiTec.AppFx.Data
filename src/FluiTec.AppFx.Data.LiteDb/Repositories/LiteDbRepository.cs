using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.LiteDb.Providers;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb.Repositories;

/// <summary> A lite database repository.</summary>
/// <typeparam name="TEntity"> Type of the entity. </typeparam>
public class LiteDbRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public LiteDbRepository(IDataService dataService, ILiteDbDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
        UnitOfWork = unitOfWork as LiteDbUnitOfWork ?? throw new InvalidOperationException();
        Collection = dataProvider.Database.GetCollection<TEntity>();
    }

    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public new LiteDbUnitOfWork UnitOfWork { get; }

    /// <summary> Gets or sets the collection.</summary>
    /// <value> The collection.</value>
    public ILiteCollection<TEntity> Collection { get; protected set; }

    /// <summary> Gets all items in this collection.</summary>
    /// <returns>An enumerator that allows foreach to be used to process all items in this collection.</returns>
    public override IEnumerable<TEntity> GetAll()
    {
        return Collection.FindAll();
    }

    /// <summary> Gets all asynchronous.</summary>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns> all.</returns>
    public override Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Collection.FindAll());
    }

    /// <summary> Gets the count.</summary>
    /// <returns> A long.</returns>
    public override long Count()
    {
        return Collection.Count();
    }

    /// <summary> Count asynchronous.</summary>
    /// <returns> The count.</returns>
    public override Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Count());
    }
}