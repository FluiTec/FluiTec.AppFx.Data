using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dapper.Mysql;

/// <summary>   A mysql dapper service options. </summary>
public class MysqlDapperServiceOptions : DapperServiceOptions
{
    /// <summary>   Default constructor. </summary>
    public MysqlDapperServiceOptions()
    {
        ConnectionFactory = new MysqlConnectionFactory();
    }

    /// <summary>   Constructor. </summary>
    /// <param name="connectionString"> The connection string. </param>
    // ReSharper disable once UnusedMember.Global
    public MysqlDapperServiceOptions(string connectionString) : base(connectionString)
    {
        ConnectionFactory = new MysqlConnectionFactory();
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
    public override SqlType SqlType => SqlType.Mysql;
}