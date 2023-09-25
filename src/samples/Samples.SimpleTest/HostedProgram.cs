using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Data.DataProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samples.TestData.DataServices;
using Samples.TestData.Entities;
using Samples.TestData.UnitsOfWork;

namespace Samples.SimpleConsole;

/// <summary>
///     Hosted program.
/// </summary>
public class HostedProgram : ConsoleHostedProgram
{
    /// <summary>   Constructor. </summary>
    /// <param name="logger">           The logger. </param>
    /// <param name="lifetime">         The lifetime. </param>
    /// <param name="providerResolver"> The provider resolver. </param>
    public HostedProgram(ILogger<ConsoleHostedProgram> logger, IHostApplicationLifetime lifetime,
        DataProviderResolver<ITestDataService, ITestUnitOfWork> providerResolver)
        : base(logger, lifetime)
    {
        DataProvider = providerResolver.Resolve();
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
        AddEntry();
        ListEntries();
    }

    public void AddEntry()
    {
        using var uow = DataProvider.BeginUnitOfWork();
        uow.DummyRepository.Add(new DummyEntity { Name = "Test Manual" });

        uow.Commit();
    }

    public void ListEntries()
    {
        using var uow = DataProvider.BeginUnitOfWork();
        foreach (var entity in uow.DummyRepository.GetAll())
        {
            Console.WriteLine($"Id: {entity.Id} - Name: {entity.Name}");
        }
    }
}