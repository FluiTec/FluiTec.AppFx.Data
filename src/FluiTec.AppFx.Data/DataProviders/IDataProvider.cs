using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataProviders;

public interface IDataProvider
{
    /// <summary>   Gets the name strategy. </summary>
    /// <value> The name strategy. </value>
    INameStrategy NameStrategy { get; }

    /// <summary>   Gets the page settings. </summary>
    /// <value> The page settings. </value>
    PageSettings PageSettings { get; }

    /// <summary>   Gets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    ProviderType ProviderType { get; }

    /// <summary>   Gets the logger. </summary>
    /// <value> The logger. </value>
    public ILogger<IDataProvider>? Logger { get; }
}

/// <summary>   Interface for data provider. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public interface IDataProvider<out TDataService, out TUnitOfWork> : IDataProvider
    where TDataService : IDataService
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Gets the data service. </summary>
    /// <value> The data service. </value>
    TDataService DataService { get; }

    /// <summary> Begins unit of work.</summary>
    /// <returns> A TUnitOfWork.</returns>
    TUnitOfWork BeginUnitOfWork();

    /// <summary> Begins unit of work.</summary>
    /// <param name="transactionOptions"> Options for controlling the transaction. </param>
    /// <returns> A TUnitOfWork.</returns>
    TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions);

    /// <summary> Begins unit of work.</summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns> A TUnitOfWork.</returns>
    TUnitOfWork BeginUnitOfWork(IUnitOfWork parentUnitOfWork);
}