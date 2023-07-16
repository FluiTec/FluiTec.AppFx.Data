using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A save changes aware. </summary>
public class SaveChangesAware : ISaveChangesAware
{
    /// <summary>   True if has changes, false if not. </summary>
    private bool _hasChanges;

    /// <summary>   Occurs when a property value changes. </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>   Event queue for all listeners interested in ModelChanged events. </summary>
    public event EventHandler? ModelChanged;

    /// <summary>   Gets or sets a value indicating whether this object has changes. </summary>
    /// <value> True if this object has changes, false if not. </value>
    public bool HasChanges
    {
        get => _hasChanges;
        set
        {
            if (value == _hasChanges) return;
            Debug.WriteLine($"{GetType().Name}:HasChanges:{value}");
            _hasChanges = value;
            OnPropertyChanged();
        }
    }

    /// <summary>   Accept changes. </summary>
    public void AcceptChanges()
    {
        HasChanges = false;
    }

    /// <summary>   Executes the 'property changed' action. </summary>
    /// <param name="propertyName"> (Optional) Name of the property. </param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Debug.WriteLine($"PropertyChanged: {propertyName}@{GetType().Name}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        if (propertyName != nameof(HasChanges))
            OnModelChanged();
    }

    /// <summary>   Executes the 'model changed' action. </summary>
    protected virtual void OnModelChanged()
    {
        Debug.WriteLine($"ModelChanged: @{GetType().Name}");
        HasChanges = true;
        ModelChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>   Sets a field. </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    /// <param name="field">        [in,out] The field. </param>
    /// <param name="value">        The value. </param>
    /// <param name="propertyName"> (Optional) Name of the property. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        Debug.WriteLine($"SetField: {propertyName} @ {value}");
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}