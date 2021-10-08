using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    /// A dapper date time dummy repository.
    /// </summary>
    public class DapperDateTimeDummyRepository : DapperWritableKeyTableDataRepository<DateTimeDummyEntity, int>, IDateTimeDummyRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperDateTimeDummyRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}