using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary>
    /// A memory dummy repository.
    /// </summary>
    public class NMemoryDummyRepository : NMemoryWritableKeyTableDataRepository<DummyEntity, int>, IDummyRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryDummyRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}