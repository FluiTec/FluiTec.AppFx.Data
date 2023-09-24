using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper writable repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class DapperWritableTableRepository<TEntity> : DapperTableRepository<TEntity>,
    IWritableTableDataRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public DapperWritableTableRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork) : base(dataService, dataProvider, unitOfWork)
    {
    }

    /// <summary>   Adds entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Add(TEntity entity)
    {
        if (TypeSchema.UsesIdentityKey)
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleAutoStatement(TypeSchema);
            var key = UnitOfWork.Connection.ExecuteScalar<object>(sql, entity, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
            var castKey = Convert.ChangeType(key, TypeSchema.IdentityKey!.PropertyType);
            TypeSchema.IdentityKey.SetValue(entity, castKey);
        }
        else
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleStatement(TypeSchema);
            UnitOfWork.Connection.Execute(sql, entity, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
        }

        return entity;
    }

    /// <summary>   Adds an asynchronous to 'cancellationToken'. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The add. </returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (TypeSchema.UsesIdentityKey)
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleAutoStatement(TypeSchema);
            var query = new CommandDefinition(sql, entity, UnitOfWork.Transaction,
                (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
            var key = await UnitOfWork.Connection.ExecuteScalarAsync<object>(query);
            var castKey = Convert.ChangeType(key, TypeSchema.IdentityKey!.PropertyType);
            TypeSchema.IdentityKey.SetValue(entity, castKey);

        }
        else
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleStatement(TypeSchema);
            var query = new CommandDefinition(sql, entity, UnitOfWork.Transaction,
                (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);

            await UnitOfWork.Connection.ExecuteAsync(query);
        }

        return entity;
    }

    /// <summary>   Adds a range. </summary>
    /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        if (TypeSchema.UsesIdentityKey)
        {
            var sql = UnitOfWork.StatementProvider.GetInsertMultipleAutoStatement(TypeSchema);
            UnitOfWork.Connection.Execute(sql, entities, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
        }
        else
        {
            var sql = UnitOfWork.StatementProvider.GetInsertMultipleStatement(TypeSchema);
            UnitOfWork.Connection.Execute(sql, entities, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
        }
    }

    /// <summary>   Adds a range asynchronous to 'cancellationToken'. </summary>
    /// <param name="entities">             An IEnumerable&lt;TEntity&gt; of items to append to this. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   A Task. </returns>
    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (TypeSchema.UsesIdentityKey)
        {
            var sql = UnitOfWork.StatementProvider.GetInsertMultipleAutoStatement(TypeSchema);
            var query = new CommandDefinition(sql, entities, UnitOfWork.Transaction,
                (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
            await UnitOfWork.Connection.ExecuteAsync(query);
        }
        else
        {
            var sql = UnitOfWork.StatementProvider.GetInsertMultipleStatement(TypeSchema);
            var query = new CommandDefinition(sql, entities, UnitOfWork.Transaction,
                (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
            await UnitOfWork.Connection.ExecuteAsync(query);
        }
    }

    /// <summary>   Gets update parameters. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   The update parameters. </returns>
    protected virtual DynamicParameters GetUpdateParameters(TEntity entity)
    {
        var parameters = new DynamicParameters();
        foreach(var p in TypeSchema.MappedProperties)
            parameters.Add(p.Name.Name, p.GetValue(entity));
        return parameters;
    }

    /// <summary>   Updates the given entity. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   A TEntity. </returns>
    public virtual TEntity Update(TEntity entity)
    {
        var sql = UnitOfWork.StatementProvider.GetUpdateStatement(TypeSchema);
        UnitOfWork.Connection.Execute(sql, GetUpdateParameters(entity), UnitOfWork.Transaction,
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
        return entity;
    }

    /// <summary>   Updates the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The update. </returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var sql = UnitOfWork.StatementProvider.GetUpdateStatement(TypeSchema);
        var query = new CommandDefinition(sql, GetUpdateParameters(entity), UnitOfWork.Transaction,
            (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
        await UnitOfWork.Connection.ExecuteAsync(query);
        return entity;
    }

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public virtual bool Delete(params object[] keys)
    {
        var namedKeys = GetNamedKey(keys);
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetDeleteStatement(TypeSchema, namedKeys);
        return UnitOfWork.Connection.Execute(sql, keyParameters, UnitOfWork.Transaction, 
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds) > 0;
    }

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="keys">                 A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The delete. </returns>
    public virtual async Task<bool> DeleteAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        var namedKeys = GetNamedKey(keys);
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetDeleteStatement(TypeSchema, namedKeys);
        var query = new CommandDefinition(sql, keyParameters, UnitOfWork.Transaction,
            (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
        return await UnitOfWork.Connection.ExecuteAsync(query) > 0;
    }

    /// <summary>   Deletes the given keys. </summary>
    /// <param name="entity">   The entity to add. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public virtual bool Delete(TEntity entity)
    {
        var namedKeys = new Dictionary<string, object>(TypeSchema.KeyProperties
            .OrderBy(kp => kp.Order)
            .Select(kp => new KeyValuePair<string, object>(kp.Name.Name, kp.GetValue(entity))));
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetDeleteStatement(TypeSchema, namedKeys);
        return UnitOfWork.Connection.Execute(sql, keyParameters, UnitOfWork.Transaction,
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds) > 0;
    }

    /// <summary>   Deletes the asynchronous. </summary>
    /// <param name="entity">               The entity to add. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The delete. </returns>
    public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var namedKeys = new Dictionary<string, object>(TypeSchema.KeyProperties
            .OrderBy(kp => kp.Order)
            .Select(kp => new KeyValuePair<string, object>(kp.Name.Name, kp.GetValue(entity))));
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetDeleteStatement(TypeSchema, namedKeys);
        var query = new CommandDefinition(sql, keyParameters, UnitOfWork.Transaction,
            (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds, cancellationToken: cancellationToken);
        return await UnitOfWork.Connection.ExecuteAsync(query) > 0;
    }
}