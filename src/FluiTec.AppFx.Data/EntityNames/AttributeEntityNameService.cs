using System;
using System.Collections.Concurrent;
using System.Linq;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.EntityNames;

/// <summary>   A service for accessing attribute entity names information. </summary>
public class AttributeEntityNameService : ClassEntityNameService
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
    public override EntityName GetName(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (_typeMap.ContainsKey(type))
            return _typeMap[type];

        var name = type.GetImmediateType().GetAttributes<EntityNameAttribute>().SingleOrDefault() is { } attribute
            ? attribute.Name
            : base.GetName(type);
        _typeMap.TryAdd(type, name);
        return name;
    }
}