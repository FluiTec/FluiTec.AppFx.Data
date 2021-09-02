using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using FluiTec.AppFx.Console.ConsoleItems;
using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Data.Dynamic.Console
{
    /// <summary>
    /// A data console module.
    /// </summary>
    public class DataConsoleModule : ModuleConsoleItem
    {
        #region Fields

        /// <summary>
        ///     (Immutable) the configuration values.
        /// </summary>
        private IOrderedEnumerable<KeyValuePair<string, string>> _configValues;

        #endregion

        #region Properties

        /// <summary>   Gets the save enabled provider. </summary>
        /// <value> The save enabled provider. </value>
        public IConfigurationProvider SaveEnabledProvider { get; }

        /// <summary>   Gets or sets the configuration root. </summary>
        /// <value> The configuration root. </value>
        private IConfigurationRoot ConfigurationRoot { get; set; }

        /// <summary>
        /// Gets or sets the data services.
        /// </summary>
        ///
        /// <value>
        /// The data services.
        /// </value>
        private List<IDataService> DataServices { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="saveEnabledProvider">  The save enabled provider. </param>
        public DataConsoleModule(IConfigurationProvider saveEnabledProvider) : base("Data")
        {
            DataMigrationSingleton.Instance.VersionMismatch -=
                DataMigrationSingleton.Instance.DefaultVersionMismatchAction;

            DataMigrationSingleton.Instance.MigrationPossible -=
                DataMigrationSingleton.Instance.DefaultMigrationPossibleAction;

            SaveEnabledProvider = saveEnabledProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes this.
        /// </summary>
        protected override void Initialize()
        {
            ConfigurationRoot = Application.HostServices.GetRequiredService<IConfigurationRoot>();

            var providers = ConfigurationRoot.Providers
                .Where(p => p.GetType() != typeof(EnvironmentVariablesConfigurationProvider));

            _configValues = new ConfigurationRoot(providers.ToList()).AsEnumerable().OrderBy(v => v.Key);
            RecreateItems();
        }

        /// <summary>
        /// Recreate items.
        /// </summary>
        private void RecreateItems()
        {
            Items.Clear();
            Items.AddRange(Application.HostServices.GetServices<IDataService>().Select(s => new DataServiceConsoleItem(s, this)));
        }

        /// <summary>
        /// Displays this.
        /// </summary>
        ///
        /// <param name="parent">   The parent. </param>
        public override void Display(IConsoleItem parent)
        {
            RecreateItems();
            base.Display(parent);
        }

        /// <summary>
        /// Configure command.
        /// </summary>
        ///
        /// <returns>
        /// A System.CommandLine.Command.
        /// </returns>
        public override Command ConfigureCommand()
        {
            var cmd = new Command("--data", "Configuration of the applications database.");
            return cmd;
        }

        #endregion

        /// <summary>
        /// Values that represent exit codes.
        /// </summary>
        public enum ExitCode
        {
            /// <summary>
            /// An enum constant representing the success option.
            /// </summary>
            Success = 0,
            /// <summary>
            /// An enum constant representing the error option.
            /// </summary>
            Error = 1
        }
    }
}