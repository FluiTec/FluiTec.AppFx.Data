namespace FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

/// <summary>   Interface for SQL keywords. </summary>
public interface ISqlKeywords
{
    /// <summary>   Gets the select. </summary>
    /// <value> The select. </value>
    string Select { get; }

    /// <summary>   Gets the update. </summary>
    /// <value> The update. </value>
    string Update { get; }

    /// <summary>   Gets the source for the. </summary>
    /// <value> from. </value>
    string From { get; }

    /// <summary>   Gets the where. </summary>
    /// <value> The where. </value>
    string Where { get; }

    /// <summary>   Gets the insert. </summary>
    /// <value> The insert. </value>
    string Insert { get; }

    /// <summary>   Gets the into. </summary>
    /// <value> The into. </value>
    string Into { get; }

    /// <summary>   Gets the values. </summary>
    /// <value> The values. </value>
    string Values { get; }

    /// <summary>   Gets the set. </summary>
    /// <value> The set. </value>
    string Set { get; }

    /// <summary>   Gets the and. </summary>
    /// <value> The and. </value>
    string And { get; }

    /// <summary>   Gets the or. </summary>
    /// <value> The or. </value>
    string Or { get; }

    /// <summary>   Gets the list separator. </summary>
    /// <value> The list separator. </value>
    string ListSeparator { get; }

    /// <summary>   Gets the command separator. </summary>
    /// <value> The command separator. </value>
    string CommandSeparator { get; }

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

    /// <summary>   Gets the assign equals operator. </summary>
    /// <value> The assign equals operator. </value>
    string AssignEqualsOperator { get; }

    /// <summary>   Gets the compare equals operator. </summary>
    /// <value> The compare equals operator. </value>
    string CompareEqualsOperator { get; }
}