using System;
using System.Collections.Concurrent;
using System.Linq;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   A service for accessing attribute Property names information. </summary>
public class AttributePropertyNameService : ClassPropertyNameService
{
    /// <summary>   (Immutable) the property map. </summary>
    private readonly ConcurrentDictionary<ImmediateProperty, PropertyName> _propertyMap = new();

    /// <summary>   (Immutable) the type map. </summary>
    private readonly ConcurrentDictionary<Type, PropertyName> _typeMap = new();

    /// <summary>   Gets a name. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="property"> The property. </param>
    /// <returns>   The name. </returns>
    public override PropertyName GetName(ImmediateProperty property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        if (_propertyMap.TryGetValue(property, out var name1))
            return name1;

        var name = property.GetAttributes<PropertyNameAttribute>().SingleOrDefault() is { } attribute
            ? new PropertyName(attribute.ColumnName, property.Name)
            : base.GetName(property);
        _propertyMap.TryAdd(property, name);
        return name;
    }
}