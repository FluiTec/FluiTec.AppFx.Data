using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Data.Dapper.SqLite;

/// <summary>   A sqlite dapper service options. </summary>
[ConfigurationKey("Dapper.Sqlite")]
public class SqliteDapperServiceOptions : DapperServiceOptions
{
    /// <summary>   Default constructor. </summary>
    // ReSharper disable once UnusedMember.Global
    public SqliteDapperServiceOptions()
    {
        ConnectionFactory = new SqliteConnectionFactory();
    }

    /// <summary>   Constructor. </summary>
    /// <param name="connectionString"> The connection string. </param>
    // ReSharper disable once UnusedMember.Global
    public SqliteDapperServiceOptions(string connectionString) : base(connectionString)
    {
        ConnectionFactory = new SqliteConnectionFactory();
    }

    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    public override IConnectionFactory ConnectionFactory { get; }

    /// <summary>
    ///     Gets the type of the SQL.
    /// </summary>
    /// <value>
    ///     The type of the SQL.
    /// </value>
    public override SqlType SqlType => SqlType.Sqlite;
}