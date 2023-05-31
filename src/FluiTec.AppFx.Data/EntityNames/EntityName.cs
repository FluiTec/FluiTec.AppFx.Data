using System;

namespace FluiTec.AppFx.Data.EntityNames;

/// <summary>   An entity name. </summary>
public class EntityName
{
    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    /// <param name="name">     The name. </param>
    public EntityName(string? schema, string name)
    {
        Schema = string.IsNullOrWhiteSpace(schema) ? null : schema;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;
    }

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public string? Schema { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; }
}