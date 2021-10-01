using DynamicSample.Data.Entities;
using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.NMemory
{
    /// <summary>
    /// A memory dummy 2 repository.
    /// </summary>
    public class NMemoryDummy2Repository : NMemoryWritableKeyTableDataRepository<DummyEntity2, int>, IDummy2Repository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryDummy2Repository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }
    }
}