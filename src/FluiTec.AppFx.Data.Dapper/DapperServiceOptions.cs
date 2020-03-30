using System;

namespace FluiTec.AppFx.Data.Dapper
{
    public class DapperServiceOptions : IDapperServiceOptions
    {
        /// <summary>   Constructor. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="connectionString">     The connection string. </param>
        /// <param name="connectionFactory">    The connection factory. </param>
        public DapperServiceOptions(string connectionString, IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} must neither be null or empty.",
                    nameof(connectionString));
            ConnectionString = connectionString;
        }

        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        public IConnectionFactory ConnectionFactory { get; }

        /// <summary>   Gets the connection string. </summary>
        /// <value> The connection string. </value>
        public string ConnectionString { get; }
    }
}