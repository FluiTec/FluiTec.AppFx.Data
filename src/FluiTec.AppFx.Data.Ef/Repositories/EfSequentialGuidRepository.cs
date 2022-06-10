using System;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Ef.Repositories;

/// <summary>
/// An ef sequential unique identifier repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public abstract class EfSequentialGuidRepository<TEntity> : EfPreDefinedKeyRepository<TEntity, Guid>
    where TEntity : class, IKeyEntity<Guid>, new()
{
    #region Constructors

    /// <summary>
    /// Specialized constructor for use only by derived class.
    /// </summary>
    ///
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected EfSequentialGuidRepository(EfUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
    {
    }

    #endregion

    #region EfPredefinedKeyRepository

    /// <summary>	Sets insert key. </summary>
    /// <param name="entity">	The entity. </param>
    protected override void SetInsertKey(TEntity entity)
    {
        entity.Id = UnitOfWork.DataService.GuidGenerator.GenerateSequentialGuid();
    }

    #endregion
}