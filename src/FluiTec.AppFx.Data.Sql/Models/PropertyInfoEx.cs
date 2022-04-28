using System;
using System.Reflection;

namespace FluiTec.AppFx.Data.Sql.Models;

/// <summary>
///     Exception for signalling property information errors.
/// </summary>
/// <typeparam name="TExtended">    Type of the extended. </typeparam>
public class PropertyInfoEx<TExtended>
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="propertyInfo"> Information describing the property. </param>
    /// <param name="extendedData"> Information describing the extended. </param>
    public PropertyInfoEx(PropertyInfo propertyInfo, TExtended extendedData)
    {
        PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        ExtendedData = extendedData;
    }

    /// <summary>
    ///     Gets information describing the property.
    /// </summary>
    /// <value>
    ///     Information describing the property.
    /// </value>
    public PropertyInfo PropertyInfo { get; }

    /// <summary>
    ///     Gets information describing the extended.
    /// </summary>
    /// <value>
    ///     Information describing the extended.
    /// </value>
    public TExtended ExtendedData { get; }

    /// <summary>
    ///     Gets a value indicating whether this object has extended data.
    /// </summary>
    /// <value>
    ///     True if this object has extended data, false if not.
    /// </value>
    public bool HasExtendedData => ExtendedData != null;
}