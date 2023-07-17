using System;
using System.Globalization;
using System.Windows.Data;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Converters;

/// <summary> A nullable int converter.</summary>
public class NullableIntConverter : IValueConverter
{
    /// <summary> Converts.</summary>
    /// <param name="value">      The value. </param>
    /// <param name="targetType"> Type of the target. </param>
    /// <param name="parameter">  The parameter. </param>
    /// <param name="culture">    The culture. </param>
    /// <returns> An object.</returns>
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        var ret = value ?? -1;
        return ret;
    }

    /// <summary> Convert back.</summary>
    /// <param name="value">      The value. </param>
    /// <param name="targetType"> Type of the target. </param>
    /// <param name="parameter">  The parameter. </param>
    /// <param name="culture">    The culture. </param>
    /// <returns> The back converted.</returns>
    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        var ret = Math.Abs((double)(value ?? -1) - -1) < 0.001 ? null : value;
        return ret;
    }
}