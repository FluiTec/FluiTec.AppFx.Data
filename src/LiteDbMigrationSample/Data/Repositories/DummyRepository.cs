using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using LiteDbMigrationSample.Data.Entities;
using Microsoft.Extensions.Logging;

namespace LiteDbMigrationSample.Data.Repositories
{
    /// <summary>   A dummy repository. </summary>
    public class DummyRepository : LiteDbWritableIntegerKeyTableDataRepository<DummyEntity>
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DummyRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}