using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNames;
using Microsoft.Extensions.Logging;

namespace Samples.TestData;

/// <summary>   Interface for test data service. </summary>
public interface ITestDataService : IDataService
{
}

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
    public string Name => nameof(TestDataService);

    /// <summary>   Gets the name service. </summary>
    /// <value> The name service. </value>
    public IEntityNameService NameService => new AttributeEntityNameService();
}