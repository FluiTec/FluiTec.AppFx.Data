using System;
using System.Collections.Concurrent;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   A service for accessing class Property names information. </summary>
public class ClassPropertyNameService : IPropertyNameService
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
    public virtual PropertyName GetName(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (_typeMap.ContainsKey(type))
            return _typeMap[type];

        var name = new PropertyName(null, type.Name);
        _typeMap.TryAdd(type, name);
        return name;
    }
}