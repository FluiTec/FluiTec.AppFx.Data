using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.DataServices;

namespace FluiTec.AppFx.Data.Options;

/// <summary>   A connection string options. </summary>
public class ConnectionStringOptions : Dictionary<string, string>
{
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