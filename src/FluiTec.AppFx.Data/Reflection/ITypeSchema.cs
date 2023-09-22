using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.EntityNames;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Interface for type schema. </summary>
public interface ITypeSchema
{
    /// <summary>   Gets the type. </summary>
    /// <value> The type. </value>
    Type Type { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    EntityName Name { get; }

    /// <summary>   Gets the properties. </summary>
    /// <value> The properties. </value>
    IReadOnlyCollection<IPropertySchema> Properties { get; }

    /// <summary>   Gets the mapped properties. </summary>
    /// <value> The mapped properties. </value>
    IReadOnlyCollection<IPropertySchema> MappedProperties { get; }

    /// <summary>   Gets the unmapped properties. </summary>
    /// <value> The unmapped properties. </value>
    IReadOnlyCollection<IPropertySchema> UnmappedProperties { get; }

    /// <summary>   Gets the key properties. </summary>
    /// <value> The key properties. </value>
    IReadOnlyCollection<IKeyPropertySchema> KeyProperties { get; }

    /// <summary>   Gets the identity key. </summary>
    /// <value> The identity key. </value>
    IKeyPropertySchema? IdentityKey { get; }

    /// <summary>   Gets a value indicating whether this object uses identity key. </summary>
    /// <value> True if uses identity key, false if not. </value>
    bool UsesIdentityKey { get; }
}