using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.TestLibrary.Schema;

namespace FluiTec.AppFx.Data.TestLibrary.Entities
{
    /// <summary>   A dummy entity. </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.DummyTable)]
    public class DummyEntity : IKeyEntity<int>, IEquatable<DummyEntity>
    {
        /// <summary>   Gets or sets the identifier. </summary>
        /// <value> The identifier. </value>
        public int Id { get; set; }

        /// <summary>   Gets or sets the name. </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        ///
        /// <param name="other">    An object to compare with this object. </param>
        ///
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
        /// parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(DummyEntity other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Id == other.Id;
        }
    }
}