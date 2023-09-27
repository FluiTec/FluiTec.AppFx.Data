using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Schemata;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Tests.Fixtures;

/// <summary>   A service for accessing test data information. </summary>
public class TestDataService : ITestDataService
{
    /// <summary>   Constructor. </summary>
    /// <param name="loggerFactory">    The logger factory. </param>
    public TestDataService(ILoggerFactory? loggerFactory)
    {
        LoggerFactory = loggerFactory;
    }

    /// <summary>   Gets the name service. </summary>
    /// <value> The name service. </value>
    public IEntityNameService EntityNameService => new AttributeEntityNameService();

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    public IPropertyNameService PropertyNameService => new AttributePropertyNameService();

    /// <summary>   Gets the logger factory. </summary>
    /// <value> The logger factory. </value>
    public ILoggerFactory? LoggerFactory { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name => nameof(TestDataService);

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public ISchema Schema => new TestSchema(EntityNameService, PropertyNameService);
}