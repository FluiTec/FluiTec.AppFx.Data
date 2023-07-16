﻿using System.Transactions;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames.NameStrategies;
using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.UnitsOfWork;

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

    TUnitOfWork BeginUnitOfWork();

    TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions);

    TUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork);
}