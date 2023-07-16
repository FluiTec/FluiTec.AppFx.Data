using System.ComponentModel;

namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   Interface for save changes aware. </summary>
public interface ISaveChangesAware : INotifyPropertyChanged
{
    /// <summary>   Gets a value indicating whether this object has changes. </summary>
    /// <value> True if this object has changes, false if not. </value>
    public bool HasChanges { get; }

    /// <summary>   Event queue for all listeners interested in ModelChanged events. </summary>
    public event EventHandler? ModelChanged;

    /// <summary>   Accept changes. </summary>
    public void AcceptChanges();
}