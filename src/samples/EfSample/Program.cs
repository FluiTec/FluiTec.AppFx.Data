using FluiTec.AppFx.Options.Programs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EfSample;

/// <summary>
/// A program.
/// </summary>
internal class Program : ValidatingConfigurationManagerProgram
{
    /// <summary>
    /// Main entry-point for this application.
    /// </summary>
    ///
    /// <param name="args"> A variable-length parameters list containing arguments. </param>
    private static void Main(params string[] args)
    {

    }

    /// <summary>
    ///     Configures the given configuration builder.
    /// </summary>
    /// <param name="configurationBuilder"> The configuration builder. </param>
    /// <returns>
    ///     An IConfigurationBuilder.
    /// </returns>
    protected override IConfigurationBuilder Configure(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.secret.json", false, true);
    }

    /// <summary>
    ///     Configure services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <returns>
    ///     A ServiceCollection.
    /// </returns>
    protected override ServiceCollection ConfigureServices(ServiceCollection services)
    {
        base.ConfigureServices(services);

        return services;
    }
}