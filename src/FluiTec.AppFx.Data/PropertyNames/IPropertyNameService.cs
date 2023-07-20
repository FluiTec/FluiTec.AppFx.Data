using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   Interface for property name service. </summary>
public interface IPropertyNameService
{
    /// <summary>   Gets a name. </summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The name. </returns>
    PropertyName GetName(Type type);
}