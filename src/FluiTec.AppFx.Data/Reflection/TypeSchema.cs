using System;
using System.Collections.Generic;
using System.Linq;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A type schema. </summary>
public class TypeSchema : ITypeSchema
{
    /// <summary>   Constructor. </summary>
    /// <param name="type"> The type. </param>
    public TypeSchema(Type type)
    {
        Type = type;

        var props = type.GetImmediateProperties().ToList();
        var mapped = new List<ImmediateProperty>();
        var keys = new List<Tuple<ImmediateProperty, EntityKeyAttribute>>();

        foreach (var prop in props)
        {
            var mapAttr = prop.GetAttribute<UnmappedAttribute>(true);
            var keyAttr = prop.GetAttribute<EntityKeyAttribute>(true);

            if (mapAttr == null)
                mapped.Add(prop);

            if (keyAttr != null)
                keys.Add(new Tuple<ImmediateProperty, EntityKeyAttribute>(prop, keyAttr));
        }

        Properties = props
            .Select(p => new PropertySchema(p.PropertyType, p.Name))
            .ToList()
            .AsReadOnly();

        MappedProperties = mapped
            .Select(p => new PropertySchema(p.PropertyType, p.Name))
            .ToList()
            .AsReadOnly();

        UnmappedProperties = Properties
            .Except(MappedProperties)
            .ToList()
            .AsReadOnly();

        KeyProperties = keys
            .Select(t => new KeyPropertySchema(t.Item1.PropertyType, t.Item1.Name, t.Item2.Order, t.Item1.GetValue))
            .OrderBy(k => k.Order)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>   Gets the type. </summary>
    /// <value> The type. </value>
    public Type Type { get; }

    /// <summary>   Gets the properties. </summary>
    /// <value> The properties. </value>
    public IReadOnlyCollection<IPropertySchema> Properties { get; }

    /// <summary>   Gets the mapped properties. </summary>
    /// <value> The mapped properties. </value>
    public IReadOnlyCollection<IPropertySchema> MappedProperties { get; }

    /// <summary>   Gets the unmapped properties. </summary>
    /// <value> The unmapped properties. </value>
    public IReadOnlyCollection<IPropertySchema> UnmappedProperties { get; }

    /// <summary>   Gets the key properties. </summary>
    /// <value> The key properties. </value>
    public IReadOnlyCollection<IKeyPropertySchema> KeyProperties { get; }
}