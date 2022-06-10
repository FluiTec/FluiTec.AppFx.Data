using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Sql.Attributes;
using FluiTec.AppFx.Data.TestLibrary.Schema;

namespace FluiTec.AppFx.Data.TestLibrary.Entities
{
    /// <summary>
    /// A dummy 2 entity.
    /// </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.Dummy2Table)]
    public class Dummy2Entity : IKeyEntity<Guid>, IEquatable<Dummy2Entity>
    {
        [SqlKey(false, 0)]
        public Guid Id { get; set; }

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
        public bool Equals(Dummy2Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Name == other.Name;
        }
    }
}