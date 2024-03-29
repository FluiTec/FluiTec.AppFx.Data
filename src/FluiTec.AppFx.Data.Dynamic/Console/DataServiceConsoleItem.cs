﻿using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data service console item.
/// </summary>
public class DataServiceConsoleItem : DataSelectConsoleItem
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="module">       The module. </param>
    public DataServiceConsoleItem(IDataService dataService, DataConsoleModule module) : base(dataService.Name,
        module)
    {
        DataService = dataService;

        if (DataService.SupportsMigration)
            Items.Add(new DataMigrationConsoleItem(dataService, module));
    }

    /// <summary>
    ///     Gets the data service.
    /// </summary>
    /// <value>
    ///     The data service.
    /// </value>
    public IDataService DataService { get; }
}