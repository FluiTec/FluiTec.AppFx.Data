using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   Attribute for Property name. </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PropertyNameAttribute : Attribute
{
    /// <summary>   Constructor. </summary>
    /// <param name="name"> The name. </param>
    public PropertyNameAttribute(string name)
    {
        Name = new PropertyName(name);
    }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public PropertyName Name { get; }
}