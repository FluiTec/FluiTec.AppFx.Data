﻿using System;
using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A property schema. </summary>
public class PropertySchema : IPropertySchema
{
    /// <summary>   Constructor. </summary>
    /// <param name="propertyType"> Type of the property. </param>
    /// <param name="name">         The name. </param>
    public PropertySchema(Type propertyType, PropertyName name)
    {
        PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>   Gets the type of the property. </summary>
    /// <value> The type of the property. </value>
    public Type PropertyType { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public PropertyName Name { get; }

    /// <summary>
    ///     Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">    An object to compare with this object. </param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" />
    ///     parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(IPropertySchema other)
    {
        return PropertyType == other.PropertyType && Name == other.Name;
    }

    /// <summary>   Determines whether the specified object is equal to the current object. </summary>
    /// <param name="obj">  The object to compare with the current object. </param>
    /// <returns>
    ///     <see langword="true" /> if the specified object  is equal to the current object;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((PropertySchema)obj);
    }

    /// <summary>   Serves as the default hash function. </summary>
    /// <returns>   A hash code for the current object. </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(PropertyType, Name);
    }
}