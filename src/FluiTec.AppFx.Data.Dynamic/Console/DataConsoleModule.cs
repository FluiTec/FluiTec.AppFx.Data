using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Dynamic.Extensions;
using FluiTec.AppFx.Options.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dynamic.Console;

/// <summary>
///     A data console module.
/// </summary>
public class DataConsoleModule : ModuleConsoleItem
{
    /// <summary>
    ///     Values that represent exit codes.
    /// </summary>
    public enum ExitCode
    {
        /// <summary>
        ///     An enum constant representing the success option.
        /// </summary>
        Success = 0,

        /// <summary>
        ///     An enum constant representing the error option.
        /// </summary>
        Error = 1
    }

    /// <summary>
    ///     Values that represent list types.
    /// </summary>
    public enum ListType
    {
        /// <summary>
        ///     An enum constant representing the services option.
        /// </summary>
        Services,

        /// <summary>
        ///     An enum constant representing the migrations option.
        /// </summary>
        Migrations
    }

    /// <summary>
    ///     Values that represent migration options.
    /// </summary>
    public enum MigrationOption
    {
        /// <summary>
        ///     An enum constant representing the apply option.
        /// </summary>
        Apply,

        /// <summary>
        ///     An enum constant representing the rollback option.
        /// </summary>
        Rollback
    }

    #region Constructors

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="saveEnabledProvider">  The save enabled provider. </param>
    public DataConsoleModule(IConfigurationProvider saveEnabledProvider) : base("Data")
    {
        SaveEnabledProvider = saveEnabledProvider;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="saveEnabledProvider">  The save enabled provider. </param>
    /// <param name="optionsModule">        The options module. </param>
    public DataConsoleModule(IConfigurationProvider saveEnabledProvider, OptionsConsoleModule optionsModule) : this(saveEnabledProvider)
    {
        OptionsModule = optionsModule;
    }

    #endregion

    #region Properties

    /// <summary>   Gets the save enabled provider. </summary>
    /// <value> The save enabled provider. </value>
    public IConfigurationProvider SaveEnabledProvider { get; }

    /// <summary>   Gets or sets the configuration root. </summary>
    /// <value> The configuration root. </value>
    private IConfigurationRoot ConfigurationRoot { get; set; }

    /// <summary>
    ///     Gets or sets the data services.
    /// </summary>
    /// <value>
    ///     The data services.
    /// </value>
    private List<IDataService> DataServices { get; set; }

    /// <summary>
    ///     (Immutable) the configuration values.
    /// </summary>
    public IOrderedEnumerable<KeyValuePair<string, string>> ConfigValues { get; private set; }

    /// <summary>
    /// Gets the options module.
    /// </summary>
    ///
    /// <value>
    /// The options module.
    /// </value>
    public OptionsConsoleModule OptionsModule { get; }

    #endregion

    #region Methods

    /// <summary>
    ///     Initializes this.
    /// </summary>
    protected override void Initialize()
    {
        DataMigrationSingleton.Instance.VersionMismatch -=
            DataMigrationSingleton.Instance.DefaultVersionMismatchAction;

        DataMigrationSingleton.Instance.MigrationPossible -=
            DataMigrationSingleton.Instance.DefaultMigrationPossibleAction;

        ConfigurationRoot = Application.HostServices.GetRequiredService<IConfigurationRoot>();

        var providers = ConfigurationRoot.Providers
            .Where(p => p.GetType() != typeof(EnvironmentVariablesConfigurationProvider));

        ConfigValues = new ConfigurationRoot(providers.ToList()).AsEnumerable().OrderBy(v => v.Key);
        RecreateItems();
    }

    /// <summary>
    ///     Recreate items.
    /// </summary>
    private void RecreateItems()
    {
        Items.Clear();

        DataServices = Application.HostServices.GetServices<IDataService>().ToList();
        Items.AddRange(DataServices.Select(s => new DataServiceConsoleItem(s, this)));
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

    /// <summary>
    ///     Configure command.
    /// </summary>
    /// <returns>
    ///     A System.CommandLine.Command.
    /// </returns>
    public override Command ConfigureCommand()
    {
        var cmd = new Command("--data", "Configuration of the database.");

        var listCmd = new Command("--list", "Lists all requested entries.")
        {
            new Argument<ListType>("listType", "Type of entries to list.")
        };
        listCmd.Handler = CommandHandler.Create(new Func<ListType, int>(ProcessList));

        var migrateCommand = new Command("--migrate", "Migrate the database.")
        {
            new Argument<string>("service", "Name of the service to migrate."),
            new Argument<long>("migration", "Version of the migration to use."),
            new Argument<MigrationOption>("option", "MigrationOption to use."),
            new Argument<bool>("preview", "Determines to use preview-mode or not.")
        };
        migrateCommand.Handler =
            CommandHandler.Create(new Func<string, long, MigrationOption, bool, int>(ProcessMigrateCommand));

        cmd.AddCommand(listCmd);
        cmd.AddCommand(migrateCommand);
        return cmd;
    }

    /// <summary>
    ///     Process the migrate command.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside
    ///     the required range.
    /// </exception>
    /// <param name="service">      Name of the service. </param>
    /// <param name="migration">    The migration version. </param>
    /// <param name="option">       The option. </param>
    /// <param name="preview">      Determines if using preview.</param>
    /// <returns>
    ///     An int.
    /// </returns>
    private int ProcessMigrateCommand(string service, long migration, MigrationOption option, bool preview)
    {
        var dataService = DataServices.SingleOrDefault(d => d.Name == service);
        if (dataService == null)
        {
            System.Console.WriteLine($"Could not find DataService named '{service}'.");
            return (int) ExitCode.Error;
        }

        if (!dataService.SupportsMigration)
        {
            System.Console.WriteLine("Service does not support migration.");
            return (int) ExitCode.Error;
        }

        var migrator = dataService.GetMigrator();
        var currentMigration = migrator.GetMigrations().SingleOrDefault(m => m.Version == migration);
        if (currentMigration == null)
        {
            System.Console.WriteLine($"Could not find Version '{migration}'.");
            return (int) ExitCode.Error;
        }

        switch (option)
        {
            case MigrationOption.Apply:
                if (!preview)
                    migrator.Migrate(currentMigration.Version);
                else
                {
                    var instructions = migrator.GetMigrationInstructions(currentMigration.Version);
                    System.Console.WriteLine();
                    System.Console.Write(instructions);
                    System.Console.WriteLine();
                }
                break;
            case MigrationOption.Rollback:
                var migrations = migrator.GetMigrations()
                    .OrderBy(m => m.Version)
                    .ToDictionary(m => m.Version, m => m.Name);
                var curIndex = migrations.IndexOfKey(currentMigration.Version);
                var prevIndex = curIndex - 1;
                var prevVersion = prevIndex >= 0 ? migrations.ElementAt(prevIndex).Key : 0;
                if (!preview)
                    migrator.Migrate(prevVersion);
                else
                {
                    var instructions = migrator.GetMigrationInstructions(prevVersion);
                    System.Console.WriteLine();
                    System.Console.Write(instructions);
                    System.Console.WriteLine();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }

        return (int) ExitCode.Success;
    }

    /// <summary>
    ///     Process the list described by listType.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when one or more arguments are outside
    ///     the required range.
    /// </exception>
    /// <param name="listType"> Type of the list. </param>
    /// <returns>
    ///     An int.
    /// </returns>
    private int ProcessList(ListType listType)
    {
        switch (listType)
        {
            case ListType.Services:
                foreach (var service in DataServices)
                    System.Console.WriteLine($"- {service.Name}");
                break;
            case ListType.Migrations:
                foreach (var service in DataServices)
                {
                    System.Console.WriteLine($"- {service.Name}");
                    if (!service.SupportsMigration) continue;
                    var migrator = service.GetMigrator();
                    foreach (var migration in migrator.GetMigrations().OrderByDescending(m => m.Version))
                        System.Console.WriteLine($"-- {migration.Version} | {migration.Name}");
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(listType), listType, null);
        }

        return (int) ExitCode.Success;
    }

    #endregion
}