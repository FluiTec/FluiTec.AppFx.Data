using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary> A lite database dummy 2 repository.</summary>
    public class LiteDbDummy2Repository : LiteDbSequentialGuidRepository<Dummy2Entity>, IDummy2Repository
    {
        /// <summary> Constructor.</summary>
        ///
        /// <param name="unitOfWork"> The unit of work. </param>
        /// <param name="logger">     The logger. </param>
        public LiteDbDummy2Repository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}