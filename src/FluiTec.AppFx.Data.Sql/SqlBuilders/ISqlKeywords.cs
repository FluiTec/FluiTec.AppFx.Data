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

    /// <summary>   Gets the list separator. </summary>
    /// <value> The list separator. </value>
    string ListSeparator { get; }
}