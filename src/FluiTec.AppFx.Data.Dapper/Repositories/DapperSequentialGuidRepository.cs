using System;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.Repositories;

/// <summary>	A dapper unique identifier repository. </summary>
// ReSharper disable once UnusedMember.Global
public abstract class DapperSequentialGuidRepository<TEntity> : DapperPreDefinedKeyRepository<TEntity, Guid>
    where TEntity : class, IKeyEntity<Guid>, new()
{
    #region Constructors

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    /// <param name="logger">       The logger. </param>
    protected DapperSequentialGuidRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
        unitOfWork, logger)
    {
    }

    #endregion

    #region DapperPredefinedKeyRepository

    /// <summary>	Sets insert key. </summary>
    /// <param name="entity">	The entity. </param>
    protected override void SetInsertKey(TEntity entity)
    {
        entity.Id = UnitOfWork.DapperDataService.GuidGenerator.GenerateSequentialGuid();
    }

    #endregion
}