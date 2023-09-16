using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>   A dapper table repository. </summary>
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public class DapperTableRepository<TEntity> : DapperPagedRepository<TEntity>, ITableRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>   Constructor. </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="dataProvider"> The data provider. </param>
    /// <param name="unitOfWork">   The unit of work. </param>
    public DapperTableRepository(IDataService dataService, IDataProvider dataProvider, IUnitOfWork unitOfWork) : base(dataService, dataProvider, unitOfWork)
    {
        KeyProperties = DataService.Schema[EntityType].KeyProperties
            .OrderBy(p => p.Order)
            .ToList();
    }

    /// <summary>   Gets the key properties. </summary>
    /// <value> The key properties. </value>
    public IList<IKeyPropertySchema> KeyProperties { get; }

    /// <summary>   Gets named key. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   The named key. </returns>
    protected IDictionary<string, object> GetNamedKey(params object[] keys)
    {
        var key = new ExpandoObject() as IDictionary<string, object>;
        for (var i = 0; i < keys.Length; i++) key.Add(KeyProperties[i].Name.ColumnName, keys[i]);
        return key;
    }

    /// <summary>   Gets key parameters. </summary>
    /// <param name="namedKeys">    The named keys. </param>
    /// <returns>   The key parameters. </returns>
    protected DynamicParameters GetKeyParameters(IDictionary<string, object> namedKeys)
    {
        var keyParameters = new DynamicParameters();
        foreach (var key in namedKeys)
            keyParameters.Add(key.Key, key.Value);
        return keyParameters;
    }

    /// <summary>   Gets a t entity using the given keys. </summary>
    /// <param name="keys"> A variable-length parameters list containing keys. </param>
    /// <returns>   A TEntity. </returns>
    public TEntity? Get(params object[] keys)
    {
        var namedKeys = GetNamedKey(keys);
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetSelectByKeyStatement(TypeSchema, namedKeys);
        
        return UnitOfWork.Connection.QuerySingleOrDefault<TEntity>(sql, keyParameters, UnitOfWork.Transaction,
            commandTimeout: (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds);
    }

    /// <summary>   Gets an asynchronous. </summary>
    /// <param name="keys">                 A variable-length parameters list containing keys. </param>
    /// <param name="cancellationToken">    (Optional) A token that allows processing to be
    ///                                     cancelled. </param>
    /// <returns>   The asynchronous. </returns>
    public Task<TEntity?> GetAsync(object[] keys, CancellationToken cancellationToken = default)
    {
        var namedKeys = GetNamedKey(keys);
        var keyParameters = GetKeyParameters(namedKeys);
        var sql = UnitOfWork.StatementProvider.GetSelectByKeyStatement(TypeSchema, namedKeys);

        var query = new CommandDefinition(sql, keyParameters, UnitOfWork.Transaction, (int)UnitOfWork.TransactionOptions.Timeout.TotalSeconds,
            cancellationToken: cancellationToken);

        return UnitOfWork.Connection.QuerySingleOrDefaultAsync<TEntity?>(query);
    }
}