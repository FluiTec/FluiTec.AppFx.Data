using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    /// A lite database date time dummy repository.
    /// </summary>
    public class LiteDbDateTimeDummyRepository : LiteDbWritableIntegerKeyTableDataRepository<DateTimeDummyEntity>, IDateTimeDummyRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbDateTimeDummyRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}