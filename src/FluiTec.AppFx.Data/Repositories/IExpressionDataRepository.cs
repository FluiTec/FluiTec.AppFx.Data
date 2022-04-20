using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data.Repositories;

/// <summary>
/// Interface for expression data repository.
/// </summary>
///
/// <typeparam name="TEntity">  Type of the entity. </typeparam>
public interface IExpressionDataRepository<TEntity> : IDataRepository<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Enumerates the items in this collection that meet given criteria.
    /// </summary>
    ///
    /// <param name="expression">   The expression. </param>
    ///
    /// <returns>
    /// An enumerator that allows foreach to be used to process the matched items.
    /// </returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
}