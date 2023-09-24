using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using ImmediateReflection;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A type schema. </summary>
public class TypeSchema : ITypeSchema
{
    /// <summary>   Constructor. </summary>
    /// <param name="type">                 The type. </param>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="propertyNameService">  The property name service. </param>
    public TypeSchema(Type type, IEntityNameService entityNameService, IPropertyNameService propertyNameService)
    {
        EntityNameService = entityNameService;
        PropertyNameService = propertyNameService;
        Type = type;
        Name = EntityNameService.GetName(type);

        var props = type.GetImmediateProperties().ToList();
        var mapped = new List<ImmediateProperty>();
        var keys = new List<Tuple<ImmediateProperty, EntityKeyAttribute>>();
        ImmediateProperty? idKey = null;

        foreach (var prop in props)
        {
            var mapAttr = prop.GetAttribute<UnmappedAttribute>(true);
            var keyAttr = prop.GetAttribute<EntityKeyAttribute>(true);
            var idKeyAttr = prop.GetAttribute<IdentityKeyAttribute>(true);

            if (mapAttr == null)
                mapped.Add(prop);

            if (keyAttr != null)
                keys.Add(new Tuple<ImmediateProperty, EntityKeyAttribute>(prop, keyAttr));

            if (idKeyAttr == null) continue;
            if (idKey != null)
                throw new NonSingularIdentityKeyException(type);
            idKey = prop;
        }

        Properties = props
            .Select(p => new PropertySchema(p.PropertyType, PropertyNameService.GetName(p), p.GetValue))
            .ToList()
            .AsReadOnly();

        MappedProperties = mapped
            .Select(p => new PropertySchema(p.PropertyType, PropertyNameService.GetName(p), p.GetValue))
            .ToList()
            .AsReadOnly();

        UnmappedProperties = Properties
            .Except(MappedProperties)
            .ToList()
            .AsReadOnly();

        KeyProperties = keys
            .Select(t => new KeyPropertySchema(t.Item1.PropertyType, PropertyNameService.GetName(t.Item1),
                t.Item2.Order, t.Item1.GetValue, t.Item1.SetValue))
            .OrderBy(k => k.Order)
            .ToList()
            .AsReadOnly();

        IdentityKey = idKey != null ? KeyProperties.Single(kp => kp.Name == PropertyNameService.GetName(idKey)) : null;
    }

    /// <summary>   Gets the entity name service. </summary>
    /// <value> The entity name service. </value>
    public IEntityNameService EntityNameService { get; }

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    public IPropertyNameService PropertyNameService { get; }

    /// <summary>   Gets the type. </summary>
    /// <value> The type. </value>
    public Type Type { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public EntityName Name { get; }

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

    /// <summary>   Gets the identity key. </summary>
    /// <value> The identity key. </value>
    public IKeyPropertySchema? IdentityKey { get; }

    /// <summary>   Gets a value indicating whether this object uses identity key. </summary>
    /// <value> True if uses identity key, false if not. </value>
    public bool UsesIdentityKey => IdentityKey != null;
}