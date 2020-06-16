using DynamicSample.Data.Entities;
using DynamicSample.Data.Repositories;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace DynamicSample.Data.LiteDb
{
    public class LiteDbDummyRepository : LiteDbWritableIntegerKeyTableDataRepository<DummyEntity>, IDummyRepository
    {
        public LiteDbDummyRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }
    }
}