using System;
using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   A key property schema. </summary>
public class KeyPropertySchema : PropertySchema, IKeyPropertySchema
{
    /// <summary>   (Immutable) the set value accessor. </summary>
    private readonly Action<object, object> _setValueAccessor;

    /// <summary>   Constructor. </summary>
    /// <param name="propertyType">     Type of the property. </param>
    /// <param name="name">             The name. </param>
    /// <param name="order">            The order. </param>
    /// <param name="getValueAccessor"> The get value accessor. </param>
    /// <param name="setValueAccessor"> The set-value accessor. </param>
    public KeyPropertySchema(Type propertyType, PropertyName name, int order, Func<object, object> getValueAccessor, Action<object, object> setValueAccessor) :
        base(propertyType, name, getValueAccessor)
    {
        _setValueAccessor = setValueAccessor;
        Order = order;
    }

    /// <summary>   Gets the order. </summary>
    /// <value> The order. </value>
    public int Order { get; }

    

    /// <summary>   Sets a value. </summary>
    /// <param name="obj">      The object. </param>
    /// <param name="value">    The value. </param>
    public void SetValue(object obj, object value)
    {
        _setValueAccessor(obj, value);
    }
}