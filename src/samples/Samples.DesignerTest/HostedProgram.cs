using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Console.Modularization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Samples.DesignerTest;

/// <summary>
///     Hosted program.
/// </summary>
public class HostedProgram : ConsoleHostedInteractiveProgram
{
    /// <summary>   Constructor. </summary>
    /// <param name="logger">   The logger. </param>
    /// <param name="lifetime"> The lifetime. </param>
    /// <param name="menus">    The menus. </param>
    public HostedProgram(ILogger<ConsoleHostedProgram> logger, IHostApplicationLifetime lifetime,
        IEnumerable<IInteractiveConsoleMenu> menus)
        : base(logger, lifetime, menus)
    {
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public override string Name => "DesignerTest";
}