using System.Linq;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.Dynamic.Extensions;
using FluiTec.AppFx.Data.Migration;
using Spectre.Console;

namespace FluiTec.AppFx.Data.Dynamic.Console
{
    /// <summary>
    ///     A data migration rollback console item.
    /// </summary>
    public class DataMigrationRollbackConsoleItem : ConsoleItem
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="info">     The information. </param>
        /// <param name="migrator"> The migrator. </param>
        public DataMigrationRollbackConsoleItem(MigrationInfo info, IDataMigrator migrator) : base("Rollback migration")
        {
            Info = info;
            Migrator = migrator;
        }

        /// <summary>
        ///     Gets the information.
        /// </summary>
        /// <value>
        ///     The information.
        /// </value>
        public MigrationInfo Info { get; }

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
            base.Display(parent);

            Presenter.PresentHeader($"Rollback migration {Name}");

            var migrations = Migrator.GetMigrations()
                .OrderBy(m => m.Version)
                .ToDictionary(m => m.Version, m => m.Name);
            var curIndex = migrations.IndexOfKey(Info.Version);
            var prevIndex = curIndex - 1;
            var prevVersion = prevIndex >= 0 ? migrations.ElementAt(prevIndex).Key : 0;

            AnsiConsole.MarkupLine(
                $">> Rolling back this migration will decrease the db version to {Presenter.HighlightText(prevVersion.ToString())}");
            AnsiConsole.MarkupLine(
                $">> {Presenter.ErrorText("This will probably cause a loss of data.")} [darkorange]Please make sure you have a backup![/]");

            if (AnsiConsole.Confirm("Are you sure?", false))
                Migrator.Migrate(prevVersion);

            Parent?.Display(null);
        }
    }
}