using System;
using System.IO;
using System.Text;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     Export apply migration console item
/// </summary>
public class DataMigrationExportApplyMigrationConsoleItem : ConsoleItem
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public DataMigrationExportApplyMigrationConsoleItem(MigrationInfo info, IDataMigrator migrator)
        : base("Export apply migration")
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

        Presenter.PresentHeader($"Export apply migration {Info.Version}");

        var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fileName = $"apply_migration_v{Info.Version}.sql";
        var output = Migrator.GetMigrationInstructions(Info.Version);

        File.WriteAllText(Path.Combine(desktopFolder, fileName), output, Encoding.Default);

        Parent?.Display(null);
    }
}