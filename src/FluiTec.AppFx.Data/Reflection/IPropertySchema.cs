using System;
using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Interface for property schema. </summary>
public interface IPropertySchema : IEquatable<IPropertySchema>
{
    /// <summary>   Gets the type of the property. </summary>
    /// <value> The type of the property. </value>
    Type PropertyType { get; }

    /// <summary>   Gets the name. </summary>
    /// <value> The name. </value>
    PropertyName Name { get; }

    /// <summary>   Gets a value. </summary>
    /// <param name="obj">  The object. </param>
    /// <returns>   The value. </returns>
    object GetValue(object obj);
}