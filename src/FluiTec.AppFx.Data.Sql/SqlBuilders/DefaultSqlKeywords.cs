namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   A default SQL keywords. </summary>
public class DefaultSqlKeywords : ISqlKeywords
{
    /// <summary>   Gets the select. </summary>
    /// <value> The select. </value>
    public virtual string Select => "SELECT";

    /// <summary>   Gets the source for the. </summary>
    /// <value> from. </value>
    public string From => "FROM";

    public string ListSeparator => ",";
}

/// <summary>   A microsoft SQL keywords. </summary>
public class MicrosoftSqlKeywords : DefaultSqlKeywords
{
}