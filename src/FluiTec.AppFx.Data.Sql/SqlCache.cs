using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.Sql.Attributes;
using FluiTec.AppFx.Data.Sql.Models;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.Sql;

/// <summary>	A SQL entity cache. </summary>
public static class SqlCache
{
    /// <summary>	The type properties. </summary>
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, IList<ImmediateProperty>> TypeProperties =
        new();

    /// <summary>	The type key properties. </summary>
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, IList<PropertyInfoEx<SqlKeyAttribute>>>
        TypeKeyProperties =
            new();

    /// <summary>	The entity name cache. </summary>
    public static ConcurrentDictionary<RuntimeTypeHandle, string> EntityNameCache =
        new();

    /// <summary>	Type properties chache. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A list of. </returns>
    public static IList<ImmediateProperty> TypePropertiesChache(Type type)
    {
        if (TypeProperties.TryGetValue(type.TypeHandle, out var propertyInfos))
            return propertyInfos;

        var allProperties = type.GetImmediateProperties();

        var properties = new List<ImmediateProperty>();
        foreach (var property in allProperties)
            if (!property.GetAttributes(typeof(SqlIgnoreAttribute)).Any())
                properties.Add(property);

        TypeProperties[type.TypeHandle] = properties.ToList();
        return properties;
    }

    /// <summary>	Type key properties cache. </summary>
    /// <param name="type">	The type. </param>
    /// <returns>	A list of. </returns>
    public static IList<PropertyInfoEx<SqlKeyAttribute>> TypeKeyPropertiesCache(Type type)
    {
        if (TypeKeyProperties.TryGetValue(type.TypeHandle, out var propertyInfos))
            return propertyInfos;

        var allProperties = TypePropertiesChache(type);

        var markedKeyProperties = allProperties
            .Select(p =>
                new PropertyInfoEx<SqlKeyAttribute>(p, p.GetAttribute<SqlKeyAttribute>()))
            .Where(p => p.HasExtendedData)
            .ToList();

        IList<PropertyInfoEx<SqlKeyAttribute>> keyProperties = markedKeyProperties.Any()
            ? markedKeyProperties
            : allProperties.Where(p => p.Name == "Id")
                .Select(p =>
                    new PropertyInfoEx<SqlKeyAttribute>(p, new SqlKeyAttribute(true, 0)))
                .ToList();

        // validate key configuration
        var keyCount = keyProperties.Count;
        var distictCount = keyProperties.Select(kp => kp.ExtendedData.Order).Distinct().Count();
        if (keyCount != distictCount)
            throw new InvalidOperationException(
                $"Entity '{type.Name} has invalid configuration. Multiple keys require using the SqlKeyAttribute using the constructor with (identityKey, order).");

        if (keyProperties.Count(p => p.ExtendedData.IdentityKey) > 1)
            throw new InvalidOperationException(
                $"Entity '{type.Name} has invalid configuration. Only one key can be an IdentityKey!");

        TypeKeyProperties[type.TypeHandle] = keyProperties;
        return keyProperties;
    }
}