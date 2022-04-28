using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Migration;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.DataServices
{
    /// <summary>   A service for accessing pgsql test data information.</summary>
    public class PgsqlDapperTestDataService : DapperTestDataService
    {
        /// <summary>   Constructor.</summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        public PgsqlDapperTestDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) :
            base(
                dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Gets the name.</summary>
        /// <value> The name.</value>
        public override string Name => nameof(PgsqlDapperTestDataService);

        /// <summary>   Gets the type of the SQL.</summary>
        /// <value> The type of the SQL.</value>
        public override SqlType SqlType => SqlType.Pgsql;
    }
}