using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>Basic, abstract implementation of an IUnitOfWork.</summary>
/// <seealso cref="FluiTec.AppFx.Data.UnitsOfWork.IUnitOfWork" />
public abstract class UnitOfWork : IUnitOfWork
{
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="UnitOfWork" /> class.</summary>
    /// <param name="dataService">The data service.</param>
    /// <param name="logger">The logger.</param>
    /// <exception cref="System.ArgumentNullException">dataService</exception>
    protected UnitOfWork(IDataService dataService, ILogger<IUnitOfWork> logger)
    {
        DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        Logger = logger; // we accept null here
        RepositoryProviders = new Dictionary<Type, Func<IUnitOfWork, ILogger<IRepository>, IRepository>>();
        Repositories = new Dictionary<Type, IRepository>();
    }

    #endregion

    #region Properties

    /// <summary>Gets the data service.</summary>
    /// <value>The data service.</value>
    public IDataService DataService { get; }

    /// <summary>Gets the logger.</summary>
    /// <value>The logger.</value>
    public ILogger<IUnitOfWork> Logger { get; }

    /// <summary>Gets the repository providers.</summary>
    /// <value>The repository providers.</value>
    protected Dictionary<Type, Func<IUnitOfWork, ILogger<IRepository>, IRepository>> RepositoryProviders { get; }

    /// <summary>Gets the repositories.</summary>
    /// <value>The repositories.</value>
    protected Dictionary<Type, IRepository> Repositories { get; }

    #endregion

    #region IUnitOfWork

    /// <summary>	Gets or sets the logger factory. </summary>
    /// <value>	The logger factory. </value>
    /// <summary>   Commits the UnitOfWork. </summary>
    public abstract void Commit();

    /// <summary>   Rolls back the UnitOfWork. </summary>
    public abstract void Rollback();

    /// <summary>Registers the repository provider.</summary>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <param name="repositoryProvider">The repository provider.</param>
    /// <exception cref="System.ArgumentNullException">repositoryProvider</exception>
    /// <exception cref="System.InvalidOperationException">A provider for {repoType.Name} was already registerd!</exception>
    // ReSharper disable once UnusedMember.Global
    protected void RegisterRepositoryProvider<TRepository>(
        Func<IUnitOfWork, ILogger<IRepository>, TRepository> repositoryProvider)
        where TRepository : class, IRepository
    {
        if (repositoryProvider == null)
            throw new ArgumentNullException(nameof(repositoryProvider));
        var repoType = typeof(TRepository);
        if (RepositoryProviders.ContainsKey(repoType))
            throw new InvalidOperationException($"A provider for {repoType.Name} was already registerd!");

        RepositoryProviders.Add(repoType, repositoryProvider);
    }

    /// <summary>   Gets a repository. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <typeparam name="TRepository">  Type of the repository. </typeparam>
    /// <returns>   The repository. </returns>
    public virtual TRepository GetRepository<TRepository>() where TRepository : class, IRepository
    {
        var repoType = typeof(TRepository);
        if (Repositories.ContainsKey(repoType)) // already created?
            return Repositories[repoType] as TRepository;

        // check if we can create one
        if (!RepositoryProviders.ContainsKey(repoType))
            throw new InvalidOperationException(
                $"No provider for {repoType.Name} registered - can't create instance!");

        // create, add to list and return
        var repo = RepositoryProviders[repoType]
            .Invoke(this, DataService.LoggerFactory?.CreateLogger<TRepository>());
        Repositories.Add(repoType, repo);
        return repo as TRepository;
    }

    /// <summary>
    ///     Gets the repository.
    /// </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <returns>
    ///     The repository.
    /// </returns>
    public IDataRepository<TEntity> GetDataRepository<TEntity>() 
        where TEntity : class, IEntity, new()
    {
        var expectedType = typeof(IDataRepository<TEntity>);
        var repoTypes = RepositoryProviders.Keys;
        foreach (var repoType in repoTypes.Where(repoType => expectedType.IsAssignableFrom(repoType)))
        {
            if (Repositories.ContainsKey(repoType)) // already created?
                return Repositories[repoType] as IDataRepository<TEntity>;

            // create, add to list and return
            var repo = RepositoryProviders[repoType]
                .Invoke(this, DataService.LoggerFactory?.CreateLogger<IDataRepository<TEntity>>());
            Repositories.Add(repoType, repo);
            return repo as IDataRepository<TEntity>;
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets writeable repository.
    /// </summary>
    ///
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    ///
    /// <returns>
    /// The writeable repository.
    /// </returns>
    public IWritableTableDataRepository<TEntity> GetWritableRepository<TEntity>()
        where TEntity : class, IEntity, new()
    {
        var expectedType = typeof(IDataRepository<TEntity>);
        var repoTypes = RepositoryProviders.Keys;

        foreach (var repoType in repoTypes.Where(repoType => expectedType.IsAssignableFrom(repoType)))
        {
            if (Repositories.ContainsKey(repoType)) // already created?
                return Repositories[repoType] as IWritableTableDataRepository<TEntity>;

            // create, add to list and return
            var repo = RepositoryProviders[repoType]
                .Invoke(this, DataService.LoggerFactory?.CreateLogger<IDataRepository<TEntity>>());
            Repositories.Add(repoType, repo);
            return repo as IWritableTableDataRepository<TEntity>;
        }

        throw new NotImplementedException();
    }

    /// <summary>
    ///     Gets writable repository.
    /// </summary>
    /// <typeparam name="TEntity">  Type of the entity. </typeparam>
    /// <typeparam name="TKey">     Type of the key. </typeparam>
    /// <returns>
    ///     The writable repository.
    /// </returns>
    public IWritableKeyTableDataRepository<TEntity, TKey> GetKeyWritableRepository<TEntity, TKey>()
        where TEntity : class, IKeyEntity<TKey>, new()
    {
        return GetDataRepository<TEntity>() as IWritableKeyTableDataRepository<TEntity, TKey>;
    }
    
    #endregion

    #region IDisposable

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    ///     resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
    ///     resources.
    /// </summary>
    /// <param name="disposing">
    ///     true to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected abstract void Dispose(bool disposing);

    #endregion
}