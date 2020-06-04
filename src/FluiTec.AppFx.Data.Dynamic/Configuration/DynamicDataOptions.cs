namespace FluiTec.AppFx.Data.Dynamic.Configuration
{
    /// <summary>   A dynamic data options. </summary>
    /// <remarks>
    /// You'll probably want to use the 'ConfigurationKey'-Attribute on derived classes
    /// </remarks>
    public class DynamicDataOptions
    {
        /// <summary>   Gets or sets the provider. </summary>
        /// <value> The provider. </value>
        public DataProvider Provider { get; set; }

        /// <summary>   Gets or sets a value indicating whether the automatic migrate. </summary>
        /// <value> True if automatic migrate, false if not. </value>
        public bool AutoMigrate { get; set; }
    }
}