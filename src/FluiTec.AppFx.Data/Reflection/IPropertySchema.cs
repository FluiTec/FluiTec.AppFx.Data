using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Interface for property schema. </summary>
public interface IPropertySchema : IEquatable<IPropertySchema>
{
    /// <summary>   Gets the type of the property. </summary>
    /// <value> The type of the property. </value>
    Type PropertyType { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    string Name { get; }
}