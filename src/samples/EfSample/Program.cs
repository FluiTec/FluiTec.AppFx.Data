using EfSample.Data;
using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Options.Programs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfSample;

/// <summary>
///     A program.
/// </summary>
internal class Program : ValidatingConfigurationManagerProgram
{
    /// <summary>
    ///     Main entry-point for this application.
    /// </summary>
    /// <param name="args"> A variable-length parameters list containing arguments. </param>
    private static void Main(params string[] args)
    {
        var prog = new Program();
        var sp = prog.GetServiceProvider();

        var service = sp.GetRequiredService<ITestDataService>();
        using (var uow = service.BeginUnitOfWork())
        {
            uow.DummyRepository.Add(new DummyEntity {Name = "Testname"});
            uow.Commit();
        }

        using (var uow = service.BeginUnitOfWork())
        {
            var e = uow.DummyRepository.GetAll();
        }
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

        services.ConfigureDynamicDataProvider<ITestDataService, DynamicDataOptions>(Manager,
            (options, provider) =>
                new EfTestDataService(
                    provider.GetRequiredService<IOptionsMonitor<EfSqlServiceOptions>>(),
                    provider.GetService<ILoggerFactory>()));

        return services;
    }
}