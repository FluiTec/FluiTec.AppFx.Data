using System;
using System.IO;
using System.Linq;
using System.Text;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.Dynamic.Extensions;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     Export rollback migration console item
/// </summary>
public class DataMigrationExportRollbackMigrationConsoleItem : ConsoleItem
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public DataMigrationExportRollbackMigrationConsoleItem(MigrationInfo info, IDataMigrator migrator)
        : base("Export rollback migration")
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
    private MigrationInfo Info { get; }

    /// <summary>
    ///     Gets the migrator.
    /// </summary>
    /// <value>
    ///     The migrator.
    /// </value>
    private IDataMigrator Migrator { get; }

    /// <summary>
    ///     Displays the Item.
    /// </summary>
    /// <param name="parent"></param>
    /// <remarks>
    ///     Exports the item to the desktop.
    /// </remarks>
    public override void Display(IConsoleItem parent)
    {
        base.Display(parent);

        Presenter.PresentHeader($"Export rollback migration {Info.Version}");

        var migrations = Migrator.GetMigrations()
            .OrderBy(m => m.Version)
            .ToDictionary(m => m.Version, m => m.Name);
        var curIndex = migrations.IndexOfKey(Info.Version);
        var prevIndex = curIndex - 1;
        var prevVersion = prevIndex >= 0 ? migrations.ElementAt(prevIndex).Key : 0;


        var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fileName = $"rollback_migration_v{prevVersion}.sql";
        var output = Migrator.GetMigrationInstructions(prevVersion);

        File.WriteAllText(Path.Combine(desktopFolder, fileName), output, Encoding.Default);

        Parent?.Display(null);
    }
}