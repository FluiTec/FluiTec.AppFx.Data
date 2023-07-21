using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Data.DataProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samples.TestData.DataServices;
using Samples.TestData.UnitsOfWork;

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
        IDataProvider<ITestDataService, ITestUnitOfWork> dataProvider)
        : base(logger, lifetime)
    {
        DataProvider = dataProvider;
    }

    /// <summary>   Gets the data provider. </summary>
    /// <value> The data provider. </value>
    public IDataProvider<ITestDataService, ITestUnitOfWork> DataProvider { get; }

    /// <summary>
    ///     Runs the given arguments.
    /// </summary>
    /// <param name="args"> The arguments. </param>
    public override void Run(string[] args)
    {
        using var uow = DataProvider.BeginUnitOfWork();
        uow.DummyRepository.GetAll();
    }
}