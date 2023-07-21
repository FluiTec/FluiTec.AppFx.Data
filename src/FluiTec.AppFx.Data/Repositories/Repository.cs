using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>   A repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    protected Repository(IDataService dataService, IDataProvider dataProvider)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

        EntityType = typeof(TEntity);
        TableName = dataProvider.NameStrategy.ToString(EntityType, dataService.EntityNameService);
        Logger = dataService.LoggerFactory?.CreateLogger<IRepository<TEntity>>();
    }

    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    public IDataService DataService { get; }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public IDataProvider DataProvider { get; }

    /// <summary>   Gets the type of the entity. </summary>
    /// <value> The type of the entity. </value>
    public Type EntityType { get; protected set; }

    /// <summary>   Gets the name of the table. </summary>
    /// <value> The name of the table. </value>
    public string TableName { get; protected set; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IRepository<TEntity>>? Logger { get; protected set; }

    /// <summary>   Gets all items in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public abstract IEnumerable<TEntity> GetAll();

    /// <summary>   Gets all asynchronous. </summary>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   all. </returns>
    public abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>   Gets the count. </summary>
    /// <returns>   A long. </returns>
    public abstract long Count();

    /// <summary>   Count asynchronous. </summary>
    /// <returns>   The count. </returns>
    public abstract Task<long> CountAsync();
}