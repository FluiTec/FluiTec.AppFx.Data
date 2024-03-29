namespace FluiTec.AppFx.Data.Sql.EventArgs;

/// <summary>	Additional information for SQL events. </summary>
public class SqlEventArgs : System.EventArgs
{
    /// <summary>	Constructor. </summary>
    /// <param name="statement">	The statement. </param>
    public SqlEventArgs(string statement)
    {
        SqlStatement = statement;
    }

    /// <summary>	Gets or sets the SQL statement. </summary>
    /// <value>	The SQL statement. </value>
    public string SqlStatement { get; set; }
}