using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A property changed base. </summary>
public class PropertyChangedBase : INotifyPropertyChanged
{
    /// <summary>   Event queue for all listeners interested in PropertyChanged events. </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>   Executes the 'property changed' action. </summary>
    /// <param name="propertyName"> (Optional) Name of the property. </param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>   Sets a field. </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    /// <param name="field">        [in,out] The field. </param>
    /// <param name="value">        The value. </param>
    /// <param name="propertyName"> (Optional) Name of the property. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}