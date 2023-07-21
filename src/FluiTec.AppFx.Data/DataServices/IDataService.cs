using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Schemata;
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

    /// <summary>   Gets the entity name service. </summary>
    /// <value> The entity name service. </value>
    IEntityNameService EntityNameService { get; }

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    IPropertyNameService PropertyNameService { get; }

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    ISchema Schema { get; }
}