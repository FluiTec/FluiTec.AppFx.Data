namespace FluiTec.AppFx.Data.TestLibrary.Configuration
{
    public class DbAdminOptions
    {
        /// <summary>   Gets or sets the server.</summary>
        /// <value> The server.</value>
        public string AdminConnectionString { get; set; }

        /// <summary>   Gets or sets the integration database.</summary>
        /// <value> The integration database.</value>
        public string IntegrationDb { get; set; }

        /// <summary>   Gets or sets the integration user.</summary>
        /// <value> The integration user.</value>
        public string IntegrationUser { get; set; }

        /// <summary>   Gets or sets the integration password.</summary>
        /// <value> The integration password.</value>
        public string IntegrationPassword { get; set; }
    }
}