namespace FluiTec.AppFx.Data.Sql.SqlBuilders.Keywords;

/// <summary>   A default SQL keywords. </summary>
public class DefaultSqlKeywords : ISqlKeywords
{
    /// <summary>   Gets the select. </summary>
    /// <value> The select. </value>
    public virtual string Select => "SELECT";

    /// <summary>   Gets the update. </summary>
    /// <value> The update. </value>
    public string Update => "UPDATE";

    /// <summary>   Gets the delete. </summary>
    /// <value> The delete. </value>
    public string Delete => "DELETE";

    /// <summary>   Gets the source for the. </summary>
    /// <value> from. </value>
    public string From => "FROM";

    /// <summary>   Gets the where. </summary>
    /// <value> The where. </value>
    public string Where => "WHERE";

    /// <summary>   Gets the insert. </summary>
    /// <value> The insert. </value>
    public string Insert => "INSERT";

    /// <summary>   Gets the into. </summary>
    /// <value> The into. </value>
    public string Into => "INTO";

    /// <summary>   Gets the values. </summary>
    /// <value> The values. </value>
    public string Values => "VALUES";

    /// <summary>   Gets the set. </summary>
    /// <value> The set. </value>
    public string Set => "SET";

    /// <summary>   Gets the and. </summary>
    /// <value> The and. </value>
    public string And => "AND";

    /// <summary>   Gets the or. </summary>
    /// <value> The or. </value>
    public string Or => "OR";

    /// <summary> Gets the list separator.</summary>
    /// <value> The list separator.</value>
    public string ListSeparator => ",";

    /// <summary>   Gets the command separator. </summary>
    /// <value> The command separator. </value>
    public string CommandSeparator => ";";

    /// <summary> Gets the total number of all expression.</summary>
    /// <value> The total number of all expression.</value>
    public string CountAllExpression => "COUNT(*)";

    /// <summary> Gets the offset expression.</summary>
    /// <value> The offset expression.</value>
    public string OffsetExpression => "OFFSET";

    /// <summary> Gets the fetch next expressions.</summary>
    /// <value> The fetch next expressions.</value>
    public string FetchNextExpressions => "FETCH NEXT";

    /// <summary> Gets the offset rows expression.</summary>
    /// <value> The offset rows expression.</value>
    public string OffsetRowsExpression => "ROWS";

    /// <summary> Gets the fetch next rows expression.</summary>
    /// <value> The fetch next rows expression.</value>
    public string FetchNextRowsExpression => "ROWS ONLY";

    /// <summary> Gets the order by expression.</summary>
    /// <value> The order by expression.</value>
    public string OrderByExpression => "ORDER BY";

    /// <summary> Gets the ascending expression.</summary>
    /// <value> The ascending expression.</value>
    public string AscendingExpression => "ASC";

    /// <summary> Gets the descending expression.</summary>
    /// <value> The descending expression.</value>
    public string DescendingExpression => "DESC";

    /// <summary>   Gets the assign equals operator. </summary>
    /// <value> The assign equals operator. </value>
    public string AssignEqualsOperator => "=";

    /// <summary>   Gets the compare equals operator. </summary>
    /// <value> The compare equals operator. </value>
    public string CompareEqualsOperator => "=";
}