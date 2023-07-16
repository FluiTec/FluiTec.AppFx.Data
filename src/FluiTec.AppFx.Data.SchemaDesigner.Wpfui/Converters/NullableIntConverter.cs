using System;
using System.Globalization;
using System.Windows.Data;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Converters;

public class NullableIntConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        var ret = value ?? -1;
        return ret;
    }

    public object ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        var ret = (double)value == -1 ? null : value;
        return ret;
    }
}