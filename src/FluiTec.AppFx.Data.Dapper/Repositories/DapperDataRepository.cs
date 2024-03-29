﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.Sql;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper data repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class DapperDataRepository<TEntity> : IDataRepository<TEntity>, IRepositoryCommandCache
    where TEntity : class, IEntity, new()
{
    #region Constructors

    /// <summary>   Constructor. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperDataRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Logger = logger; // we accept null here

        SqlBuilder = unitOfWork.DapperDataService.SqlBuilder;
        EntityType = typeof(TEntity);
        TableName = GetTableName();
    }

    #endregion

    #region IRepositoryCommandCache

    /// <summary>
    ///     Gets from cache.
    /// </summary>
    /// <param name="commandFunc">  The command function. </param>
    /// <param name="memberName">   (Optional) Name of the member. </param>
    /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
    /// <returns>
    ///     The data that was read from the cache.
    /// </returns>
    public string GetFromCache(Func<string> commandFunc, [CallerMemberName] string memberName = null,
        params string[] parameters)
    {
        return UnitOfWork.DapperDataService.GetFromCache(GetType(),
            !parameters.Any() ? memberName : memberName + string.Join('.', parameters), commandFunc);
    }

    #endregion

    #region Properties

    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public DapperUnitOfWork UnitOfWork { get; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IRepository> Logger { get; }

    /// <summary>	Gets the type of the entity. </summary>
    /// <value>	The type of the entity. </value>
    public Type EntityType { get; }

    /// <summary>   Gets the name of the table. </summary>
    /// <value> The name of the table. </value>
    public virtual string TableName { get; }

    /// <summary>	Gets the SQL builder. </summary>
    /// <value>	The SQL builder. </value>
    protected ISqlBuilder SqlBuilder { get; }

    #endregion

    #region Methods

    /// <summary>   Gets table name. </summary>
    /// <returns>   The table name. </returns>
    protected string GetTableName()
    {
        return GetTableName(EntityType);
    }

    /// <summary>	Gets table name. </summary>
    /// <returns>	The table name. </returns>
    protected string GetTableName(Type t)
    {
        return SqlBuilder.Adapter.RenderTableName(t);
    }

    #endregion

    #region IDataRepository

    /// <summary>   Gets all entities in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public virtual IEnumerable<TEntity> GetAll()
    {
        Logger?.LogTrace("GetAll<{type}>", typeof(TEntity));
        return UnitOfWork.Connection.GetAll<TEntity>(SqlBuilder, UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Gets all asynchronous.
    /// </summary>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ctx = default)
    {
        Logger?.LogTrace("GetAllAsync<{type}>", typeof(TEntity));
        return UnitOfWork.Connection.GetAllAsync<TEntity>(SqlBuilder, UnitOfWork.Transaction, cancellationToken: ctx);
    }

    /// <summary>
    /// Gets the paged in this collection.
    /// </summary>
    ///
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    ///
    /// <returns>
    /// An enumerator that allows foreach to be used to process the paged in this collection.
    /// </returns>
    public virtual IEnumerable<TEntity> GetPaged(int pageIndex, int pageSize)
    {
        Logger?.LogTrace("GetPaged<{type}>(pageIndex: {pageIndex}, pageSize: {pageSize})", typeof(TEntity), pageIndex, pageSize);
        return UnitOfWork.Connection.GetPaged<TEntity>(SqlBuilder, pageIndex, pageSize, UnitOfWork.Transaction);
    }

    /// <summary>
    /// Gets paged asynchronous.
    /// </summary>
    ///
    /// <param name="pageIndex">    Zero-based index of the page. </param>
    /// <param name="pageSize">     Size of the page. </param>
    /// <param name="ctx">          (Optional) A token that allows processing to be cancelled. </param>
    ///
    /// <returns>
    /// The paged.
    /// </returns>
    public virtual Task<IEnumerable<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
        CancellationToken ctx = default)
    {
        Logger?.LogTrace("GetPagedAsync<{type}>(pageIndex: {pageIndex}, pageSize: {pageSize})", typeof(TEntity), pageIndex, pageSize);
        return UnitOfWork.Connection.GetPagedAsync<TEntity>(SqlBuilder, pageIndex, pageSize, UnitOfWork.Transaction, cancellationToken: ctx);
    }

    /// <summary>   Gets the count. </summary>
    /// <returns>   An int. </returns>
    public virtual int Count()
    {
        Logger?.LogTrace("Count<{type}>", typeof(TEntity));
        var command = GetFromCache(() => $"SELECT COUNT(*) FROM {TableName}");
        return UnitOfWork.Connection.ExecuteScalar<int>(command, null, UnitOfWork.Transaction);
    }

    /// <summary>
    ///     Count asynchronous.
    /// </summary>
    /// <param name="ctx">  (Optional) A token that allows processing to be cancelled. </param>
    /// <returns>
    ///     The count.
    /// </returns>
    public virtual Task<int> CountAsync(CancellationToken ctx = default)
    {
        Logger?.LogTrace("CountAsync<{type}>", typeof(TEntity));
        var command = GetFromCache(() => $"SELECT COUNT(*) FROM {TableName}", nameof(Count));
        return UnitOfWork.Connection.ExecuteScalarAsync<int>(new CommandDefinition(command, null,
            UnitOfWork.Transaction, cancellationToken: ctx));
    }

    #endregion
}