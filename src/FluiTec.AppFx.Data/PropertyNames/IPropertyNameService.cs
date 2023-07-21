using ImmediateReflection;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   Interface for property name service. </summary>
public interface IPropertyNameService
{
    /// <summary>   Gets a name. </summary>
    /// <param name="property"> The property. </param>
    /// <returns>   The name. </returns>
    PropertyName GetName(ImmediateProperty property);
}