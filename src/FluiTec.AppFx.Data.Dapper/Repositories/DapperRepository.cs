using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class DapperRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    public DapperRepository(IDataService dataService, IDataProvider dataProvider) : base(dataService, dataProvider)
    {
    }

    /// <summary>   Gets all items in this collection. </summary>
    /// <returns>
    ///     An enumerator that allows foreach to be used to process all items in this collection.
    /// </returns>
    public override IEnumerable<TEntity> GetAll()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>   Gets all asynchronous. </summary>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   all. </returns>
    public override Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>   Gets the count. </summary>
    /// <returns>   A long. </returns>
    public override long Count()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>   Count asynchronous. </summary>
    /// <returns>   The count. </returns>
    public override Task<long> CountAsync()
    {
        throw new System.NotImplementedException();
    }
}