using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.LiteDb.Repositories;

/// <summary>
/// A lite database sequential unique identifier repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class
    LiteDbSequentialGuidRepository<TEntity> : LiteDbPreDefinedKeyRepository<TEntity, Guid>
    where TEntity : class, IKeyEntity<Guid>, new()
{
    #region Constructors

    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected LiteDbSequentialGuidRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

    #endregion

    #region LiteDbPredefinedKeyRepository

    /// <summary>
    /// Sets insert key.
    /// </summary>
    ///
    /// <param name="entity">   The entity to add. </param>
    protected override void SetInsertKey(TEntity entity)
    {
        entity.Id = UnitOfWork.LiteDbDataService.GuidGenerator.GenerateSequentialGuid();
    }

    #endregion
}