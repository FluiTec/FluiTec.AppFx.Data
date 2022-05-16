using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data migration version console item.
/// </summary>
public class DataMigrationVersionConsoleItem : DataSelectConsoleItem
{
    #region Properties

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
    ///     Gets the data service.
    /// </summary>
    /// <value>
    ///     The data service.
    /// </value>
    public IDataService DataService { get; }

    /// <summary>
    ///     Gets the name of the display.
    /// </summary>
    /// <value>
    ///     The name of the display.
    /// </value>
    public override string DisplayName { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    /// <param name="info">         The information. </param>
    /// <param name="migrator">     The migrator. </param>
    /// <param name="dataService">  The data service. </param>
    /// <param name="module">       The module. </param>
    public DataMigrationVersionConsoleItem(MigrationInfo info, IDataMigrator migrator, IDataService dataService,
        DataConsoleModule module) : base($"{info.Version} | {info.Name}", module)
    {
        Info = info;
        Migrator = migrator;
        DataService = dataService;
        var isCurrentVersion = Migrator.CurrentVersion == Info.Version;
        DisplayName = isCurrentVersion ? $"{info.Version} | {info.Name} | <current>" : $"{info.Version} | {info.Name}";
    }

    /// <summary>
    ///     Recreate items.
    /// </summary>
    private void RecreateItems()
    {
        Items.Clear();

        if (Info.Version != Migrator.CurrentVersion)
        {
            Items.Add(new DataMigrationApplyConsoleItem(Info, Migrator));
            Items.Add(new DataMigrationExportApplyMigrationConsoleItem(Info, Migrator));
        }

        // ReSharper disable once InvertIf // reason: readability
        if (Migrator.CurrentVersion >= Info.Version)
        {
            Items.Add(new DataMigrationRollbackConsoleItem(Info, Migrator));
            Items.Add(new DataMigrationExportRollbackMigrationConsoleItem(Info, Migrator));
        }
    }

    /// <summary>
    ///     Displays this.
    /// </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IConsoleItem parent)
    {
        RecreateItems();

        base.Display(parent);
    }

    #endregion
}