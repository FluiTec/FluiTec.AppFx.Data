using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing project files information. </summary>
public class ProjectFileService : IProjectFileService
{
    /// <summary>   The current file. </summary>
    private string? _currentFile;

    /// <summary>   Occurs when a property value changes. </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>   Gets or sets the current file. </summary>
    /// <value> The current file. </value>
    public string? CurrentFile
    {
        get => _currentFile;
        set
        {
            if (_currentFile != value)
            {
                if (!string.IsNullOrEmpty(_currentFile) && !File.Exists(value))
                {

                }
                _currentFile = value;
                OnPropertyChanged();
            }
        }
    }

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