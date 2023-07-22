using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Schemata;

/// <summary>   A schema. </summary>
public abstract class Schema : ISchema
{
    /// <summary>   The schemata. </summary>
    protected readonly ConcurrentDictionary<Type, Lazy<ITypeSchema>> Schemata = new();

    /// <summary>   Specialized default constructor for use only by derived class. </summary>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="propertyNameService">  The property name service. </param>
    protected Schema(IEntityNameService entityNameService, IPropertyNameService propertyNameService)
    {
        EntityNameService = entityNameService;
        PropertyNameService = propertyNameService;
    }

    /// <summary>   Gets the entity name service. </summary>
    /// <value> The entity name service. </value>
    public IEntityNameService EntityNameService { get; }

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    public IPropertyNameService PropertyNameService { get; }

    /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="entityType">   Type of the entity. </param>
    /// <returns>   The indexed item. </returns>
    public virtual ITypeSchema this[Type entityType]
    {
        get
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            if (!Schemata.ContainsKey(entityType))
                throw new MissingEntitySchemaException(this, entityType);

            return Schemata.TryGetValue(entityType, out var schema)
                ? schema.Value
                : new TypeSchema(entityType, EntityNameService, PropertyNameService);
        }
    }

    /// <summary>   Returns an enumerator that iterates through the collection. </summary>
    /// <returns>   An enumerator that can be used to iterate through the collection. </returns>
    public IEnumerator<ITypeSchema> GetEnumerator()
    {
        return Schemata.Values.Select(v => v.Value).GetEnumerator();
    }

    /// <summary>   Returns an enumerator that iterates through a collection. </summary>
    /// <returns>
    ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate
    ///     through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>   Adds an entity. </summary>
    /// <param name="entityType">   Type of the entity. </param>
    protected void AddEntity(Type entityType)
    {
        if (!Schemata.ContainsKey(entityType))
            Schemata.TryAdd(entityType,
                new Lazy<ITypeSchema>(() => new TypeSchema(entityType, EntityNameService, PropertyNameService)));
    }
}