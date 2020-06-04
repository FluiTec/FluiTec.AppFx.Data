using System;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace LiteDbSample.Data
{
    /// <summary>   A service for accessing test databases information. </summary>
    public class TestDbService : LiteDbDataService<TestUnitOfWork>
    {
        /// <summary>   Constructor. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public TestDbService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        public override string Name => "TestDbService";

        /// <summary>   Gets a value indicating whether the supports migration. </summary>
        /// <value> True if supports migration, false if not. </value>
        public override bool SupportsMigration => true;

        public override TestUnitOfWork BeginUnitOfWork()
        {
            return new TestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        public override TestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is LiteDbUnitOfWork))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(LiteDbUnitOfWork)}!");
            return new TestUnitOfWork(this, (LiteDbUnitOfWork) other, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }
    }
}