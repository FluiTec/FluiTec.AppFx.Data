﻿using System.Transactions;
using FluiTec.AppFx.Data.NMemory.Providers;
using FluiTec.AppFx.Data.UnitsOfWork;
using NMemory;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;

/// <summary>   A configurable n memory test data provider. </summary>
public class ConfigurableNMemoryTestDataProvider : ConfigurableNMemoryDataProvider<ITestDataService, ITestUnitOfWork>, ITestDataProvider
{
    /// <summary>   Constructor. </summary>
    /// <param name="configureDelegate">    The configure delegate. </param>
    /// <param name="dataService">          The data service. </param>
    public ConfigurableNMemoryTestDataProvider(Func<Database> configureDelegate, ITestDataService dataService) : base(configureDelegate, dataService)
    {
    }

    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork()
    {
        return new NMemoryTestUnitOfWork(DataService, this, new TransactionOptions());
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions)
    {
        return new NMemoryTestUnitOfWork(DataService, this, transactionOptions);
    }

    /// <summary>   Begins unit of work. </summary>
    /// <param name="parentUnitOfWork"> The parent unit of work. </param>
    /// <returns>   A TUnitOfWork. </returns>
    public override ITestUnitOfWork BeginUnitofWork(IUnitOfWork parentUnitOfWork)
    {
        return new NMemoryTestUnitOfWork(DataService, this, parentUnitOfWork);
    }
}