using System;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
/// An ef SQL service options.
/// </summary>
[ConfigurationKey("FluiTec.EntityFramework")]
public class EfSqlServiceOptions : ISqlServiceOptions
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public EfSqlServiceOptions()
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
    ///                                         illegal values. </exception>
    ///
    /// <param name="connectionString"> The connection string. </param>
    /// <param name="sqlType">          Type of the SQL. </param>
    public EfSqlServiceOptions(string connectionString, SqlType sqlType)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException($"{nameof(connectionString)} must neither be null or empty!",
                nameof(connectionString));
        ConnectionString = connectionString;
        SqlType = sqlType;
    }

    /// <summary>   Gets the connection string. </summary>
    /// <value> The connection string. </value>
    [ConfigurationSecret]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets the type of the SQL.
    /// </summary>
    ///
    /// <value>
    /// The type of the SQL.
    /// </value>
    public SqlType SqlType { get; }
}