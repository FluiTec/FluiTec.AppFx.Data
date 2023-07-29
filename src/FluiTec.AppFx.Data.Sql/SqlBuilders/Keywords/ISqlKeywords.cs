namespace FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

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

    /// <summary> Gets the total number of all expression.</summary>
    /// <value> The total number of all expression.</value>
    string CountAllExpression { get; }

    /// <summary> Gets the offset expression.</summary>
    /// <value> The offset expression.</value>
    string OffsetExpression { get; }

    /// <summary> Gets the fetch next expressions.</summary>
    /// <value> The fetch next expressions.</value>
    string FetchNextExpressions { get; }

    /// <summary> Gets the offset rows expression.</summary>
    /// <value> The offset rows expression.</value>
    string OffsetRowsExpression { get; }

    /// <summary> Gets the fetch next rows expression.</summary>
    /// <value> The fetch next rows expression.</value>
    string FetchNextRowsExpression { get; }

    /// <summary> Gets the order by expression.</summary>
    /// <value> The order by expression.</value>
    string OrderByExpression { get; }

    /// <summary> Gets the ascending expression.</summary>
    /// <value> The ascending expression.</value>
    string AscendingExpression { get; }

    /// <summary> Gets the descending expression.</summary>
    /// <value> The descending expression.</value>
    string DescendingExpression { get; }
}