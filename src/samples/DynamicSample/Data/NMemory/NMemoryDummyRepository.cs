using DynamicSample.Data.Entities;
using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.NMemory
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