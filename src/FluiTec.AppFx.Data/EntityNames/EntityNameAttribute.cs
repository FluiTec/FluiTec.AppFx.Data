using System;

namespace FluiTec.AppFx.Data.EntityNames;

/// <summary>   Attribute for entity name. </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EntityNameAttribute : Attribute
{
    /// <summary>   Constructor. </summary>
    /// <param name="name"> The name. </param>
    public EntityNameAttribute(string name)
    {
        Name = new EntityName(null, name);
    }

    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    /// <param name="name">     The name. </param>
    public EntityNameAttribute(string schema, string name)
    {
        Name = new EntityName(schema, name);
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public EntityName Name { get; }
}