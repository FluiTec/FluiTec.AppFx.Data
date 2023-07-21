using System;
using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A key property schema. </summary>
public class KeyPropertySchema : PropertySchema, IKeyPropertySchema
{
    private readonly Func<object, object> _valueAccessor;

    /// <summary>   Constructor. </summary>
    /// <param name="propertyType">     Type of the property. </param>
    /// <param name="name">             The name. </param>
    /// <param name="order">            The order. </param>
    /// <param name="valueAccessor">    The value accessor. </param>
    public KeyPropertySchema(Type propertyType, PropertyName name, int order, Func<object, object> valueAccessor) :
        base(
            propertyType, name)
    {
        _valueAccessor = valueAccessor;
        Order = order;
    }

    /// <summary>   Gets the order. </summary>
    /// <value> The order. </value>
    public int Order { get; }

    /// <summary>   Gets a value. </summary>
    /// <param name="obj">  The object. </param>
    /// <returns>   The value. </returns>
    public object GetValue(object obj)
    {
        return _valueAccessor.Invoke(obj);
    }
}