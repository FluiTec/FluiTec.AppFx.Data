namespace FluiTec.AppFx.Data.Dapper
{
    /// <summary>   Interface for dapper service options. </summary>
    public interface IDapperServiceOptions
    {
        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        IConnectionFactory ConnectionFactory { get; }

        /// <summary>   Gets the connection string. </summary>
        /// <value> The connection string. </value>
        string ConnectionString { get; }
    }
}