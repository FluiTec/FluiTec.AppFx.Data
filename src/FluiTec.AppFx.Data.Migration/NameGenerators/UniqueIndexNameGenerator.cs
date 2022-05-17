namespace FluiTec.AppFx.Data.Migration.NameGenerators;

/// <summary>
///     Name generator for unique indexes.
/// </summary>
public static class UniqueIndexNameGenerator
{
    private const string Prefix = "UX";

    /// <summary>
    ///     Creates a name for an unique index.
    /// </summary>
    /// <param name="schema">   Schema to use.</param>
    /// <param name="table">    Table to use.</param>
    /// <param name="property"> Property to use.</param>
    /// <returns></returns>
    public static string CreateName(string schema, string table, string property)
    {
        return $"{Prefix}_{schema}_{table}_{property}";
    }
}