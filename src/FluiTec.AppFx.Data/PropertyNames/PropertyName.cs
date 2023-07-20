using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   An Property name. </summary>
public class PropertyName
{
    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    /// <param name="name">     The name. </param>
    public PropertyName(string? schema, string name)
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