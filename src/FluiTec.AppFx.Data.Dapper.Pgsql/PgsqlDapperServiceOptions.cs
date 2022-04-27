using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Data.Dapper.Pgsql;

/// <summary>   A pgsql dapper service options. </summary>
[ConfigurationKey("Dapper.Pgsql")]
public class PgsqlDapperServiceOptions : DapperServiceOptions
{
    /// <summary>   Default constructor. </summary>
    public PgsqlDapperServiceOptions()
    {
        ConnectionFactory = new PgsqlConnectionFactory();
    }

    /// <summary>   Constructor. </summary>
    /// <param name="connectionString"> The connection string. </param>
    // ReSharper disable once UnusedMember.Global
    public PgsqlDapperServiceOptions(string connectionString) : base(connectionString)
    {
        ConnectionFactory = new PgsqlConnectionFactory();
    }

    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    public override IConnectionFactory ConnectionFactory { get; }

    /// <summary>
    /// Gets the type of the SQL.
    /// </summary>
    ///
    /// <value>
    /// The type of the SQL.
    /// </value>
    public override SqlType SqlType => SqlType.Pgsql;
}