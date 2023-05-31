using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.TestData;
using Samples.TestData.NMemory;

namespace Samples.SimpleConsole;

/// <summary>
///     A startup.
/// </summary>
public class Startup : DefaultStartup
{
    /// <summary>
    ///     Configure services.
    /// </summary>
    /// <param name="context">  The context. </param>
    /// <param name="services"> The services. </param>
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddHostedService<HostedProgram>();

        services.AddDataService<ITestDataService, TestDataService>();

        services.AddSingleton<ITestDataProvider, NMemoryTestDataProvider>();
    }
}