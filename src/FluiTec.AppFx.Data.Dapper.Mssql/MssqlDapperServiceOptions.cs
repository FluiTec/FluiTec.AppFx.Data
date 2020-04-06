namespace FluiTec.AppFx.Data.Dapper.Mssql
{
    /// <summary>   A mssql dapper service options. </summary>
    public class MssqlDapperServiceOptions : DapperServiceOptions
    {
        /// <summary>   Default constructor. </summary>
        public MssqlDapperServiceOptions()
        {
            ConnectionFactory = new MssqlConnectionFactory();
        }

        /// <summary>   Constructor. </summary>
        /// <param name="connectionString"> The connection string. </param>
        public MssqlDapperServiceOptions(string connectionString) : base(connectionString)
        {
            ConnectionFactory = new MssqlConnectionFactory();
        }

        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        public override IConnectionFactory ConnectionFactory { get; }
    }
}
