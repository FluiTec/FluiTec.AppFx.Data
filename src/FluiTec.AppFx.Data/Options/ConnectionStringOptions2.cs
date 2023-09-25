using FluiTec.AppFx.Data.DataServices;
using System;
using System.Collections.Generic;

namespace FluiTec.AppFx.Data.Options;

/// <summary>   A connection string options 2. </summary>
public class ConnectionStringOptions2 : Dictionary<string, string>
{
}

/// <summary>   A connection string options 2. </summary>
/// <typeparam name="TDataService"> Type of the data service. </typeparam>
public class ConnectionStringOptions2<TDataService> : ConnectionStringOptions2
    where TDataService : IDataService
{
    /// <summary>   Gets the type of the data service. </summary>
    /// <value> The type of the data service. </value>
    public Type DataServiceType { get; } = typeof(TDataService);
}