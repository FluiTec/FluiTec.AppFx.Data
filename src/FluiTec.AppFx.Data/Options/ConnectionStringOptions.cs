using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.DataServices;
using System;

namespace FluiTec.AppFx.Data.Options;

/// <summary>   A connection string options. </summary>
public class ConnectionStringOptions
{
    /// <summary>   Gets or sets the type of the provider. </summary>
    /// <value> The type of the provider. </value>
    public ProviderType ProviderType { get; set; } = ProviderType.Undefined;

    /// <summary>   Gets or sets the connection string. </summary>
    /// <value> The connection string. </value>
    public string ConnectionString { get; set; } = string.Empty;
}

/// <summary>   A connection string options. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
public class ConnectionStringOptions<TDataService> : ConnectionStringOptions
    where TDataService : IDataService
{
    /// <summary>   Gets the type of the data service. </summary>
    /// <value> The type of the data service. </value>
    public Type DataServiceType { get; } = typeof(TDataService);
}