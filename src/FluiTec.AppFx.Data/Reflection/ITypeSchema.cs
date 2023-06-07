using System;
using System.Collections.Generic;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Interface for type schema. </summary>
public interface ITypeSchema
{
    /// <summary>   Gets the type. </summary>
    /// <value> The type. </value>
    Type Type { get; }

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
}