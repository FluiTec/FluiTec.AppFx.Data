using System;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data;

/// <summary>   Exception for signalling update errors.</summary>
public class UpdateException : Exception
{
    /// <summary>   Constructor.</summary>
    /// <param name="entity">   The entity. </param>
    public UpdateException(IEntity entity) : base("Update could not change entity. No entities affected by update.")
    {
        Entity = entity;
    }

    /// <summary> Constructor.</summary>
    ///
    /// <param name="entity">         The entity. </param>
    /// <param name="innerException"> The inner exception. </param>
    public UpdateException(IEntity entity, Exception innerException) : base("Update could not change entity. No entities affected by update.", innerException)
    {
        Entity = entity;
    }

    /// <summary>   Gets the entity.</summary>
    /// <value> The entity.</value>
    public IEntity Entity { get; }
}