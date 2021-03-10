using System;
using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Data.Dapper
{
    /// <summary>   A dapper service options. </summary>
    [ConfigurationKey("Dapper")]
    public abstract class DapperServiceOptions : IDapperServiceOptions
    {
        /// <summary>   Specialized default constructor for use only by derived class. </summary>
        protected DapperServiceOptions()
        {
        }

        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or
        ///     illegal values.
        /// </exception>
        /// <param name="connectionString"> The connection string. </param>
        protected DapperServiceOptions(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} must neither be null or empty!",
                    nameof(connectionString));
            ConnectionString = connectionString;
        }
        
        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        public abstract IConnectionFactory ConnectionFactory { get; }

        /// <summary>   Gets the connection string. </summary>
        /// <value> The connection string. </value>
        [ConfigurationSecret]
        public string ConnectionString { get; set; }
    }
}