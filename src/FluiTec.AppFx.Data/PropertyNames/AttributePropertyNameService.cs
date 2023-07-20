using System;
using System.Collections.Concurrent;
using System.Linq;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   A service for accessing attribute Property names information. </summary>
public class AttributePropertyNameService : ClassPropertyNameService
{
    /// <summary>   (Immutable) the type map. </summary>
    private readonly ConcurrentDictionary<Type, PropertyName> _typeMap = new();

    /// <summary>   Gets a name. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="type"> The type. </param>
    /// <returns>   The name. </returns>
    public override PropertyName GetName(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (_typeMap.ContainsKey(type))
            return _typeMap[type];

        var name = type.GetImmediateType().GetAttributes<PropertyNameAttribute>().SingleOrDefault() is { } attribute
            ? attribute.Name
            : base.GetName(type);
        _typeMap.TryAdd(type, name);
        return name;
    }
}