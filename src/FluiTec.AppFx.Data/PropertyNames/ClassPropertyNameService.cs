using System;
using System.Collections.Concurrent;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   A service for accessing class Property names information. </summary>
public class ClassPropertyNameService : IPropertyNameService
{
    /// <summary>   (Immutable) the property map. </summary>
    private readonly ConcurrentDictionary<ImmediateProperty, PropertyName> _propertyMap = new();

    /// <summary>   Gets a name. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="property"> The property. </param>
    /// <returns>   The name. </returns>
    public virtual PropertyName GetName(ImmediateProperty property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        if (_propertyMap.TryGetValue(property, out var name1))
            return name1;

        var name = new PropertyName(property.Name, property.Name);
        _propertyMap.TryAdd(property, name);
        return name;
    }
}