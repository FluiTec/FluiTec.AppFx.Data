using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Schemata;

/// <summary>   A schema. </summary>
public abstract class Schema : ISchema
{
    /// <summary>   The schemata. </summary>
    private readonly ConcurrentDictionary<Type, Lazy<ITypeSchema>> _schemata = new();

    /// <summary>   Specialized default constructor for use only by derived class. </summary>
    protected Schema()
    {
    }

    /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is
    ///     invalid.
    /// </exception>
    /// <param name="entityType">   Type of the entity. </param>
    /// <returns>   The indexed item. </returns>
    public ITypeSchema this[Type entityType]
    {
        get
        {
            if (entityType == null)
                throw new ArgumentNullException(nameof(entityType));

            if (!_schemata.ContainsKey(entityType))
                throw new MissingEntitySchemaException(this, entityType);

            return _schemata.TryGetValue(entityType, out var schema) ? schema.Value : new TypeSchema(entityType);
        }
    }

    /// <summary>   Returns an enumerator that iterates through the collection. </summary>
    /// <returns>   An enumerator that can be used to iterate through the collection. </returns>
    public IEnumerator<ITypeSchema> GetEnumerator()
    {
        return _schemata.Values.Select(v => v.Value).GetEnumerator();
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
        if (!_schemata.ContainsKey(entityType))
            _schemata.TryAdd(entityType, new Lazy<ITypeSchema>(() => new TypeSchema(entityType)));
    }
}