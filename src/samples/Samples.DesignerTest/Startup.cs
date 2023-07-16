using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Console.Modularization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.DesignerTest.Menus;

namespace Samples.DesignerTest;

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
        services.AddTransient<IInteractiveConsoleMenu, ProjectItem>();
    }
}