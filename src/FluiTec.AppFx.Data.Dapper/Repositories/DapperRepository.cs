using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class DapperRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public DapperRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork)
        : base(dataService, dataProvider, unitOfWork)
    {
        UnitOfWork = unitOfWork as DapperUnitOfWork ?? throw new InvalidOperationException();
    }

    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public new DapperUnitOfWork UnitOfWork { get; }

    /// <summary>   Gets all items in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public override IEnumerable<TEntity> GetAll()
    {
        var sql = UnitOfWork.StatementProvider.GetAllStatement(TypeSchema);
        return UnitOfWork.Connection.Query<TEntity>(sql, null, UnitOfWork.Transaction,
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
    }

    /// <summary>   Gets all asynchronous. </summary>
    /// <param name="cancellationToken">
    ///     (Optional) A token that allows processing to be
    ///     cancelled.
    /// </param>
    /// <returns>   all. </returns>
    public override Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sql = UnitOfWork.StatementProvider.GetAllStatement(TypeSchema);
        var query = new CommandDefinition(sql, null, UnitOfWork.Transaction,
            (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds,
            cancellationToken: cancellationToken);
        return UnitOfWork.Connection.QueryAsync<TEntity>(query);
    }

    /// <summary>   Gets the count. </summary>
    /// <returns>   A long. </returns>
    public override long Count()
    {
        throw new NotImplementedException();
    }

    /// <summary>   Count asynchronous. </summary>
    /// <returns>   The count. </returns>
    public override Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }
}