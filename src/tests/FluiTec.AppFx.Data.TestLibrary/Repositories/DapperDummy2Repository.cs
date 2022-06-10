using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.TestLibrary.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.TestLibrary.Repositories
{
    /// <summary> A dapper dummy 2 repository.</summary>
    public class DapperDummy2Repository : DapperSequentialGuidRepository<Dummy2Entity>, IDummy2Repository
    {
        /// <summary> Constructor.</summary>
        ///
        /// <param name="unitOfWork"> The unit of work. </param>
        /// <param name="logger">     The logger. </param>
        public DapperDummy2Repository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }
    }
}