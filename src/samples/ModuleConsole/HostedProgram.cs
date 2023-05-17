using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Console.Modularization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ModuleConsole;

/// <summary>
///     Hosted program.
/// </summary>
public class HostedProgram : ConsoleHostedModuleProgram
{
    /// <summary>   Constructor. </summary>
    /// <param name="logger">       The logger. </param>
    /// <param name="lifetime">     The lifetime. </param>
    /// <param name="commands">     The commands. </param>
    /// <param name="menus">        The menus. </param>
    /// <param name="menuItems">    The menu items. </param>
    /// <param name="moduleItems">  The module items. </param>
    public HostedProgram(ILogger<ConsoleHostedProgram> logger, IHostApplicationLifetime lifetime,
        IEnumerable<IConsoleCommand> commands, IEnumerable<IInteractiveConsoleMenu> menus,
        IEnumerable<IWindowMenuItem> menuItems, IEnumerable<IWindowModuleItem> moduleItems)
        : base(logger, lifetime, commands, menus, menuItems, moduleItems)
    {
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public override string Name => "ModuleConsole";
}