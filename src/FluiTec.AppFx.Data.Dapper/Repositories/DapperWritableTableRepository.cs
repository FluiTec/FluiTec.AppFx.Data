using System.Collections.Generic;
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
    public TEntity Add(TEntity entity)
    {
        if (TypeSchema.UsesIdentityKey)
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleAutoStatement(TypeSchema);
            var key = UnitOfWork.Connection.ExecuteScalar<object>(sql, entity, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);

            // TODO: assign key to entity
        }
        else
        {
            var sql = UnitOfWork.StatementProvider.GetInsertSingleStatement(TypeSchema);
            UnitOfWork.Connection.Execute(sql, entity, UnitOfWork.Transaction,
                commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
        }

        throw new System.NotImplementedException();

        return entity;
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        throw new System.NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
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