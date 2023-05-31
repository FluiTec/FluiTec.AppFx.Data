using System;
using System.Collections.Concurrent;

namespace FluiTec.AppFx.Data.EntityNames;

/// <summary>   A service for accessing class entity names information. </summary>
public class ClassEntityNameService : IEntityNameService
{
    /// <summary>   (Immutable) the type map. </summary>
    private readonly ConcurrentDictionary<Type, EntityName> _typeMap = new();

    /// <summary>   Gets a name. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="type"> The type. </param>
    /// <returns>   The name. </returns>
    public virtual EntityName GetName(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (_typeMap.ContainsKey(type))
            return _typeMap[type];

        var name = new EntityName(null, type.Name);
        _typeMap.TryAdd(type, name);
        return name;
    }
}