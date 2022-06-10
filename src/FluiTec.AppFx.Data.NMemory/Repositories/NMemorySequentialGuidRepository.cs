using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.NMemory.Repositories;

/// <summary>
/// A memory sequential unique identifier repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class NMemorySequentialGuidRepository<TEntity> : NMemoryPreDefinedKeyRepository<TEntity, Guid>
    where TEntity : class, IKeyEntity<Guid>, new()
{
    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected NMemorySequentialGuidRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

    #region NMemoryPredefinedKeyRepository

    /// <summary>
    /// Sets insert key.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    protected override void SetInsertKey(TEntity entity)
    {
        entity.Id = UnitOfWork.NMemoryDataService.GuidGenerator.GenerateSequentialGuid();
    }

    #endregion
}