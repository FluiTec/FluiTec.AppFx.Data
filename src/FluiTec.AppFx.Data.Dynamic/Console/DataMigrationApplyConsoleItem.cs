using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.Migration;
using Spectre.Console;

namespace FluiTec.AppFx.Data.Dynamic.Console
{
    /// <summary>
    /// A data migration apply console item.
    /// </summary>
    public class DataMigrationApplyConsoleItem : ConsoleItem
    {
        /// <summary>
        /// Gets the information.
        /// </summary>
        ///
        /// <value>
        /// The information.
        /// </value>
        public MigrationInfo Info { get; }

        /// <summary>
        /// Gets the migrator.
        /// </summary>
        ///
        /// <value>
        /// The migrator.
        /// </value>
        public IDataMigrator Migrator { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="info">     The information. </param>
        /// <param name="migrator"> The migrator. </param>
        public DataMigrationApplyConsoleItem(MigrationInfo info, IDataMigrator migrator) : base("Apply migration")
        {
            Info = info;
            Migrator = migrator;
        }

        /// <summary>
        /// Displays this.
        /// </summary>
        ///
        /// <param name="parent">   The parent. </param>
        public override void Display(IConsoleItem parent)
        {
            base.Display(parent);

            Presenter.PresentHeader($"Apply migration {Name}");

            if (Info.Version > Migrator.CurrentVersion)
            {
                AnsiConsole.MarkupLine($">> Applying this migration will update the db version to {Presenter.HighlightText("increase")}");
                AnsiConsole.MarkupLine($">> from [gold3]{Migrator.CurrentVersion}[/] to [green]{Info.Version}[/].");
                AnsiConsole.MarkupLine( ">> [darkorange]Please make sure you have a backup![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($">> Applying this migration will update the db version to {Presenter.ErrorText("decrease")}");
                AnsiConsole.MarkupLine($">> from [gold3]{Migrator.CurrentVersion}[/] to [red]{Info.Version}[/]");
                AnsiConsole.MarkupLine($">> {Presenter.ErrorText("This will probably cause a loss of data.")} [darkorange]Please make sure you have a backup![/]");
            }

            if (AnsiConsole.Confirm("Are you sure?", false))
                Migrator.Migrate(Info.Version);

            Parent?.Display(null);
        }
    }
}