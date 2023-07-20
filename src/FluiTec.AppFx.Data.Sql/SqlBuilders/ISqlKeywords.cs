namespace FluiTec.AppFx.Data.Sql.SqlBuilders;

/// <summary>   Interface for SQL keywords. </summary>
public interface ISqlKeywords
{
    /// <summary>   Gets the select. </summary>
    /// <value> The select. </value>
    string Select { get; }

    /// <summary>   Gets the source for the. </summary>
    /// <value> from. </value>
    string From { get; }
}

/// <summary>   A default SQL keywords. </summary>
public class DefaultSqlKeywords : ISqlKeywords
{
    /// <summary>   Gets the select. </summary>
    /// <value> The select. </value>
    public virtual string Select => "SELECT";

    /// <summary>   Gets the source for the. </summary>
    /// <value> from. </value>
    public string From => "FROM";
}