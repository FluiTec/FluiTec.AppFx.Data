using System.Linq;
using FluiTec.AppFx.Console;
using FluiTec.AppFx.Console.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data console extension.
/// </summary>
public static class DataConsoleExtension
{
    /// <summary>
    ///     An IServiceCollection extension method that configure data console module.
    /// </summary>
    /// <param name="services"> The services to act on. </param>
    public static void ConfigureDataConsoleModule(this IServiceCollection services)
    {
        ConsoleHost.ConfigureModule(services, provider =>
        {
            var conf = provider.GetRequiredService<IConfigurationRoot>();
            var cp = conf.Providers.SingleOrDefault(p => p is SaveableJsonConfigurationProvider);
            return new DataConsoleModule(cp);
        });
    }
}