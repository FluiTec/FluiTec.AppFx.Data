namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Interface for key property schema. </summary>
public interface IKeyPropertySchema : IPropertySchema
{
    /// <summary>   Gets the order. </summary>
    /// <value> The order. </value>
    int Order { get; }

    /// <summary>   Gets a value. </summary>
    /// <param name="obj">  The object. </param>
    /// <returns>   The value. </returns>
    object GetValue(object obj);

    /// <summary>   Sets a value. </summary>
    /// <param name="obj">      The object. </param>
    /// <param name="value">    The value. </param>
    void SetValue(object obj, object value);
}