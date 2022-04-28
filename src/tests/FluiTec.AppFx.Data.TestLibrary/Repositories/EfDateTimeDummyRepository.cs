using FluiTec.AppFx.Data.Ef.Repositories;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    ///     An ef date time dummy repository.
    /// </summary>
    public class EfDateTimeDummyRepository : EfWritableKeyTableDataRepository<DateTimeDummyEntity, int>,
        IDateTimeDummyRepository
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public EfDateTimeDummyRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }
    }
}