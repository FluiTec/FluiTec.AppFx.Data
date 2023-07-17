using System;
using System.Transactions;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Options;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Data.LiteDb.Providers;

/// <summary>   A lite database data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public class LiteDbDataProvider<TDataService, TUnitOfWork> : BaseDataProvider<TDataService, TUnitOfWork>, ILiteDbDataProvider,
    IDataProvider<TDataService, TUnitOfWork>
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    public LiteDbDataProvider(TDataService dataService, DataOptions options) : base(dataService, options)
    {
    }

    public LiteDbDataProvider(TDataService dataService, IOptionsMonitor<DataOptions> optionsMonitor) : base(dataService, optionsMonitor)
    {
    }

    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public override ProviderType ProviderType => ProviderType.NMemory;

    public override TUnitOfWork BeginUnitOfWork()
    {
        throw new NotImplementedException();
    }

    public override TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        throw new NotImplementedException();
    }

    public override TUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork)
    {
        throw new NotImplementedException();
    }

    

    public LiteDatabase Database { get; }
}