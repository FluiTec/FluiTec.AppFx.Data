using System;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Paging;

namespace FluiTec.AppFx.Data.Options;

/// <summary>   A data options. </summary>
public class DataOptions
{
    /// <summary>   Gets or sets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public ProviderType ProviderType { get; set; }

    /// <summary>   Gets or sets the page size. </summary>
    /// <value> The size of the page. </value>
    public int PageSize { get; set; } = PageSettings.DefaultPageSize;

    /// <summary>   Gets or sets the maximum size of the page. </summary>
    /// <value> The maximum size of the page. </value>
    public int MaxPageSize { get; set; } = PageSettings.DefaultMaxPageSize;
}

/// <summary>   A data options. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
public class DataOptions<TDataService> : DataOptions
    where TDataService : IDataService
{
    /// <summary>   Gets the type of the data service. </summary>
    /// <value> The type of the data service. </value>
    public Type DataServiceType { get; } = typeof(TDataService);
}