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
using static Dapper.SqlMapper;

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
    public TEntity Add(TEntity entity)
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
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
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
    public void AddRange(IEnumerable<TEntity> entities)
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
    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
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

    public TEntity Update(TEntity entity)
    {
        throw new System.NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public bool Delete(params object[] keys)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public bool Delete(TEntity entity)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}