namespace FluiTec.AppFx.Data.Dynamic.Configuration;

/// <summary>
/// Interface for dynamic data options.
/// </summary>
public interface IDynamicDataOptions
{
    /// <summary>
    /// Gets or sets the provider.
    /// </summary>
    ///
    /// <value>
    /// The provider.
    /// </value>
    public DataProvider Provider { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the automatic migrate.
    /// </summary>
    ///
    /// <value>
    /// True if automatic migrate, false if not.
    /// </value>
    public bool AutoMigrate { get; set; }
}