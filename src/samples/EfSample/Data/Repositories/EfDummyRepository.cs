using EfSample.Data.Entities;
using FluiTec.AppFx.Data.Ef.Repositories;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace EfSample.Data.Repositories;

/// <summary>
///     An ef dummy repository.
/// </summary>
public class EfDummyRepository : EfWritableKeyTableDataRepository<DummyEntity, int>, IDummyRepository
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    public EfDummyRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }
}