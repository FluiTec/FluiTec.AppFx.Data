using FluiTec.AppFx.Console.Hosting;
using Microsoft.Extensions.Hosting;

namespace ModuleConsole;

/// <summary>
///     A program.
/// </summary>
internal class Program
{
    /// <summary>
    ///     Main entry-point for this application.
    /// </summary>
    /// <param name="args"> The arguments. </param>
    private static async Task Main(string[] args)
    {
        await CreateHostBuilder(args)
            .RunConsoleAsync();
    }

    /// <summary>
    ///     Creates host builder.
    /// </summary>
    /// <param name="args"> The arguments. </param>
    /// <returns>
    ///     The new host builder.
    /// </returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host
            .CreateDefaultBuilder(args)
            .UseStartup(new Startup());

        return builder;
    }
}