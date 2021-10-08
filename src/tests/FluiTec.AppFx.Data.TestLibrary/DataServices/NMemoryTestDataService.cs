using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using NMemory;
using NMemory.Tables;

namespace FluiTec.AppFx.Data.TestLibrary.DataServices
{
    /// <summary>
    /// A service for accessing memory test data information.
    /// </summary>
    public class NMemoryTestDataService : NMemoryDataService<NMemoryTestUnitOfWork>, ITestDataService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="loggerFactory">    The logger factory. </param>
        public NMemoryTestDataService(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public override string Name => nameof(NMemoryTestDataService);

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override NMemoryTestUnitOfWork BeginUnitOfWork()
        {
            return new NMemoryTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
        ///
        /// <param name="other">    The other. </param>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override NMemoryTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is NMemoryUnitOfWork work))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(NMemoryUnitOfWork)}!");
            return new NMemoryTestUnitOfWork(work, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <param name="other">    The other. </param>
        /// 
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        /// <returns>
        /// A TUnitOfWork.
        /// </returns>
        ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>
        /// Configure database.
        /// </summary>
        ///
        /// <param name="database"> The database. </param>
        ///
        /// <returns>
        /// A Database.
        /// </returns>
        protected override Database ConfigureDatabase(Database database)
        {
            database.Tables.Create(e => e.Id, new IdentitySpecification<DummyEntity>(e => e.Id));
            database.Tables.Create(e => e.Id, new IdentitySpecification<DateTimeDummyEntity>(e => e.Id));
            return database;
        }
    }
}