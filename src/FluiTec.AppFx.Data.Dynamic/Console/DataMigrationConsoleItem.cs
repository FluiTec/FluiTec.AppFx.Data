using System.Linq;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;
using Spectre.Console;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data migration console item.
/// </summary>
public sealed class DataMigrationConsoleItem : DataSelectConsoleItem
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="dataService">  The data service. </param>
    /// <param name="module">       The module. </param>
    public DataMigrationConsoleItem(IDataService dataService, DataConsoleModule module) : base("Migration", module)
    {
        DataService = dataService;
        Migrator = dataService.GetMigrator();

        Items.AddRange(Migrator.GetMigrations()
            .OrderByDescending(m => m.Version)
            .Select(m => new DataMigrationVersionConsoleItem(m, Migrator, dataService, Module)));
    }

    /// <summary>
    ///     Gets the data service.
    /// </summary>
    /// <value>
    ///     The data service.
    /// </value>
    public IDataService DataService { get; }

    /// <summary>
    ///     Gets the migrator.
    /// </summary>
    /// <value>
    ///     The migrator.
    /// </value>
    public IDataMigrator Migrator { get; }

    /// <summary>
    ///     Displays this.
    /// </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IConsoleItem parent)
    {
        AnsiConsole.MarkupLine($"Current Version: {Presenter.HighlightText(Migrator.CurrentVersion.ToString())}");
        AnsiConsole.MarkupLine($"Maximum Version: {Presenter.HighlightText(Migrator.MaximumVersion.ToString())}");
        AnsiConsole.MarkupLine("Available versions, ordered by version <desc>:");
        base.Display(parent);
    }
}