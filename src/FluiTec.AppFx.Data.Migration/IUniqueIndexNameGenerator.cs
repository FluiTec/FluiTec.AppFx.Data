namespace FluiTec.AppFx.Data.Migration;

public static class UniqueIndexNameGenerator
{
    private const string Prefix = "UX";
    
    public static string CreateName(string schema, string table, string property)
    {
        return $"{Prefix}_{schema}_{table}_{property}";
    }
}