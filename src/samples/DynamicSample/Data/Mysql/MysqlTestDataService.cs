using System;
using DynamicSample.Data.Dapper;
using DynamicSample.Data.Schema;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.Mysql
{
    /// <summary>   A service for accessing mysql test data information. </summary>
    public class MysqlTestDataService : DapperDataService<DapperTestUnitOfWork>, ITestDataService
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        public MysqlTestDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) : base(
            dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Gets the schema. </summary>
        /// <value> The schema. </value>
        public override string Schema => SchemaGlobals.Schema;

        /// <summary>   Gets the type of the SQL. </summary>
        /// <value> The type of the SQL. </value>
        public override SqlType SqlType => SqlType.Mysql;

        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        public override string Name => nameof(MysqlTestDataService);

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   A TUnitOfWork. </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>   Begins unit of work. </summary>
        /// <param name="other">    The other. </param>
        /// <returns>   A TUnitOfWork. </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   An IUnitOfWork. </returns>
        public override DapperTestUnitOfWork BeginUnitOfWork()
        {
            return new DapperTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="other">    The other. </param>
        /// <returns>   An IUnitOfWork. </returns>
        public override DapperTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is DapperUnitOfWork))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(DapperUnitOfWork)}!");
            return new DapperTestUnitOfWork((DapperUnitOfWork) other, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }
    }
}