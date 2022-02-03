namespace FluiTec.AppFx.Data.Migration;

/// <summary>
///     Information about the migration.
/// </summary>
public class MigrationInfo
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="version">  The version. </param>
    /// <param name="name">     The name. </param>
    public MigrationInfo(long version, string name)
    {
        Version = version;
        Name = name;
    }

    /// <summary>
    ///     Gets the version.
    /// </summary>
    /// <value>
    ///     The version.
    /// </value>
    public long Version { get; }

    /// <summary>
    ///     Gets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    public string Name { get; }
}