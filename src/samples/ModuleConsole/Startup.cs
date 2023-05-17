using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Console.Modularization;
using FluiTec.AppFx.Console.Modularization.WindowItems.DefaultItems;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModuleConsole.Commands;
using ModuleConsole.Menus;
using ModuleConsole.Modules;

namespace ModuleConsole;

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

        // command
        services.AddTransient<IConsoleCommand, GreetCommand>();

        // interactive
        services.AddTransient<IInteractiveConsoleMenu, NameMenu>();

        // windowed
        services.AddTransient<IWindowMenuItem, FileWindowMenuItem>();
        services.AddTransient<IWindowMenuItem, QuitWindowMenuItem>();
        services.AddTransient<IWindowModuleItem, DataModule>();
    }
}