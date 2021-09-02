using Cli.InteractiveSample.Data.Entities;
using Cli.InteractiveSample.Data.Repositories;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Cli.InteractiveSample.Data.Dapper
{
    public class DapperDummy2Repository : DapperWritableKeyTableDataRepository<DummyEntity2, int>, IDummy2Repository
    {
        public DapperDummy2Repository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }
    }
}