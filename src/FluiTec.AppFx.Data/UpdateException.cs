using System;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Data
{
    /// <summary>   Exception for signalling update errors.</summary>
    public class UpdateException : Exception
    {
        /// <summary>   Constructor.</summary>
        /// <param name="entity">   The entity. </param>
        public UpdateException(IEntity entity) : base("Update could not change entity. No entities affected by update.")
        {
            Entity = entity;
        }

        /// <summary>   Gets the entity.</summary>
        /// <value> The entity.</value>
        public IEntity Entity { get; }
    }
}