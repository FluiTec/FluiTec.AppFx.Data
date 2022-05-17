namespace FluiTec.AppFx.Data.Migration.NameGenerators;

/// <summary>
///     Name generator for forein keys.
/// </summary>
public static class ForeignKeyIndexNameGenerator
{
    private const string Prefix = "FK";

    /// <summary>
    ///     Creates a new name for a foreign key.
    /// </summary>
    /// <param name="schema">       Schema to use.</param>
    /// <param name="tableTo">      Table that contains the primary key to reference.</param>
    /// <param name="tableFrom">    Table that uses a foreign key reference.</param>
    /// <returns></returns>
    public static string CreateName(string schema, string tableTo, string tableFrom)
    {
        return $"{Prefix}_{schema}_{tableTo}_{tableFrom}";
    }
}