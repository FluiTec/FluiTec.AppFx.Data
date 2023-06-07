using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Schemas;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.DataServices;

/// <summary>   Interface for data service. </summary>
public interface IDataService
{
    /// <summary>   Gets the logger factory. </summary>
    /// <value> The logger factory. </value>
    ILoggerFactory? LoggerFactory { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    string Name { get; }

    /// <summary>   Gets the name service. </summary>
    /// <value> The name service. </value>
    IEntityNameService NameService { get; }

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    ISchema Schema { get; }
}