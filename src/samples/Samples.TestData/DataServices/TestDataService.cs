using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Schemas;
using Microsoft.Extensions.Logging;
using Samples.TestData.Schemas;

namespace Samples.TestData.DataServices;

/// <summary>   A service for accessing test data information. </summary>
public class TestDataService : ITestDataService
{
    /// <summary>   Constructor. </summary>
    /// <param name="loggerFactory">    The logger factory. </param>
    public TestDataService(ILoggerFactory? loggerFactory)
    {
        LoggerFactory = loggerFactory;
    }

    /// <summary>   Gets the logger factory. </summary>
    /// <value> The logger factory. </value>
    public ILoggerFactory? LoggerFactory { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name => nameof(ITestDataService);

    /// <summary>   Gets the name service. </summary>
    /// <value> The name service. </value>
    public IEntityNameService NameService => new AttributeEntityNameService();

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public ISchema Schema { get; } = new TestSchema();
}