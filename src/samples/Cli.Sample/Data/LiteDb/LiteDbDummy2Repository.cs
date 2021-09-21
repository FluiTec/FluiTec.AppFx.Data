using Cli.Sample.Data.Entities;
using Cli.Sample.Data.Repositories;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Cli.Sample.Data.LiteDb
{
    public class LiteDbDummy2Repository : LiteDbWritableIntegerKeyTableDataRepository<DummyEntity2>, IDummy2Repository
    {
        public LiteDbDummy2Repository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }
    }
}