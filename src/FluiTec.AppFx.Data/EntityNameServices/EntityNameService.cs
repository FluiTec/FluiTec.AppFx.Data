namespace FluiTec.AppFx.Data.EntityNameServices;

/// <summary>
/// A service for accessing entity names information.
/// </summary>
public static class EntityNameService
{
    /// <summary>
    /// Gets the default.
    /// </summary>
    ///
    /// <returns>
    /// The default.
    /// </returns>
    public static IEntityNameService GetDefault() => new AttributeEntityNameService();
}