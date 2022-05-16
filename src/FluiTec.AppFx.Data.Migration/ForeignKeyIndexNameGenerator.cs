namespace FluiTec.AppFx.Data.Migration;

public static class ForeignKeyIndexNameGenerator
{
    private const string Prefix = "FK";

    public static string CreateName(string schema, string tableTo,string tableFrom)
    {
        return $"{Prefix}_{schema}_{tableTo}_{tableFrom}";
    }
}