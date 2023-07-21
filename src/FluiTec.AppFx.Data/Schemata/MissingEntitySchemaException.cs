using System;

namespace FluiTec.AppFx.Data.Schemata;

/// <summary>   Exception for signalling missing entity schema errors. </summary>
public class MissingEntitySchemaException : SchemaException
{
    /// <summary>   Constructor. </summary>
    /// <param name="schema">       The schema. </param>
    /// <param name="entityType">   The type of the entity. </param>
    public MissingEntitySchemaException(ISchema schema, Type entityType) : base(schema)
    {
        EntityType = entityType;
    }

    /// <summary>   Gets the type of the entity. </summary>
    /// <value> The type of the entity. </value>
    public Type EntityType { get; }
}