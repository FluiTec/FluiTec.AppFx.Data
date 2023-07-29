using FluiTec.AppFx.Console.Hosting;
using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.StatementBuilders;
using FluiTec.AppFx.Data.Sql.StatementProviders;
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
        var provider1 = new MicrosoftSqlStatementBuilder();
        var provider2 = new CachingStatementProvider(provider1);

        var typeSchema = new TypeSchema(typeof(DummyEntity), new AttributeEntityNameService(),
            new AttributePropertyNameService());

        provider1.SqlProvided += (sender, eventArgs) => Console.WriteLine($"P1: {eventArgs.Sql}");
        provider2.SqlProvided += (sender, eventArgs) => Console.WriteLine($"P2: {eventArgs.Sql}");

        Console.WriteLine($"1: {provider1.GetAllStatement(typeSchema)}");
        //Console.WriteLine($"2: {provider2.GetAllStatement(typeSchema)}");
    }
}