using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A property schema. </summary>
public class PropertySchema : IPropertySchema
{
    /// <summary>   Constructor. </summary>
    /// <param name="propertyType"> Type of the property. </param>
    /// <param name="name">         The name. </param>
    public PropertySchema(Type propertyType, string name)
    {
        PropertyType = propertyType ?? throw new ArgumentNullException(nameof(propertyType));
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>   Gets the type of the property. </summary>
    /// <value> The type of the property. </value>
    public Type PropertyType { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; }
}