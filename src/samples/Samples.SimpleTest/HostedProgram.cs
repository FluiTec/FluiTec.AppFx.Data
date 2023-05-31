using FluiTec.AppFx.Console.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samples.TestData;

namespace Samples.SimpleConsole;

/// <summary>
///     Hosted program.
/// </summary>
public class HostedProgram : ConsoleHostedProgram
{
    /// <summary>   Constructor. </summary>
    /// <param name="logger">       The logger. </param>
    /// <param name="lifetime">     The lifetime. </param>
    /// <param name="dataProvider"> The data provider. </param>
    public HostedProgram(ILogger<ConsoleHostedProgram> logger, IHostApplicationLifetime lifetime,
        ITestDataProvider dataProvider)
        : base(logger, lifetime)
    {
        DataProvider = dataProvider;
    }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public ITestDataProvider DataProvider { get; }

    /// <summary>
    ///     Runs the given arguments.
    /// </summary>
    /// <param name="args"> The arguments. </param>
    public override void Run(string[] args)
    {
        using var uow = DataProvider.BeginUnitOfWork();

        Console.WriteLine("Dummies in Database:");
        foreach (var dummy in uow.DummyRepository.GetAll())
            Console.WriteLine($"Dummy: {dummy.Id}:{dummy.Name}");
    }
}