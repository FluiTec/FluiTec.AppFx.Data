using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.TestData.Dapper.Mssql;
using Samples.TestData.DataServices;
using Samples.TestData.NMemory;
using Samples.TestData.UnitsOfWork;

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

        services.AddAppFxDataService<ITestDataService, TestDataService>()
            .WithConfiguration(context.Configuration)
            .WithProvider<ITestUnitOfWork, NMemoryTestDataProvider>()
            .WithProvider<ITestUnitOfWork, MssqlTestDataProvider>()
            ;
    }
}