using FluiTec.AppFx.Data.Ef.Repositories;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary> An ef dummy 2 repository.</summary>
    public class EfDummy2Repository : EfSequentialGuidRepository<Dummy2Entity>, IDummy2Repository
    {
        /// <summary> Constructor.</summary>
        ///
        /// <param name="unitOfWork"> The unit of work. </param>
        /// <param name="logger">     The logger. </param>

        public EfDummy2Repository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}