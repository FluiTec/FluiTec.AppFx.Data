using System;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.TestLibrary.Schema;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.DataServices
{
    /// <summary>   A service for accessing dapper test data information.</summary>
    public abstract class DapperTestDataService : DapperDataService<DapperTestUnitOfWork>, ITestDataService
    {
        /// <summary>   Specialized constructor for use only by derived class.</summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected DapperTestDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Begins unit of work.</summary>
        /// <returns>   An IUnitOfWork.</returns>
        public override DapperTestUnitOfWork BeginUnitOfWork()
        {
            return new DapperTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work.</summary>
        /// <param name="other">    The other. </param>
        /// <returns>   A TUnitOfWork.</returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>   Begins unit of work.</summary>
        /// <returns>   A TUnitOfWork.</returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>   Begins unit of work.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
        /// <param name="other">    The other. </param>
        /// <returns>   An IUnitOfWork.</returns>
        public override DapperTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is DapperUnitOfWork))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(DapperUnitOfWork)}!");
            return new DapperTestUnitOfWork((DapperUnitOfWork)other, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Gets the schema.</summary>
        /// <value> The schema.</value>
        public override string Schema => SchemaGlobals.Schema;
    }
}