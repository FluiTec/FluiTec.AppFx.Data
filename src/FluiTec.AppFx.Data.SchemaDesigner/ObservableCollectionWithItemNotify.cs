using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

// ReSharper disable PossibleMultipleEnumeration

namespace FluiTec.AppFx.Data.SchemaDesigner;

/// <summary>
///     An observable collection with item notify. This class cannot be inherited.
/// </summary>
/// <typeparam name="T">    Generic type parameter. </typeparam>
public sealed class ObservableCollectionWithItemNotify<T> : ObservableCollection<T> where T : ISaveChangesAware
{
    /// <summary>   Default constructor. </summary>
    public ObservableCollectionWithItemNotify()
    {
        CollectionChanged += Items_CollectionChanged;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="collection">   The collection. </param>
    public ObservableCollectionWithItemNotify(IEnumerable<T> collection) : base(collection)
    {
        CollectionChanged += Items_CollectionChanged;
        foreach (var item in collection)
            item.ModelChanged += Item_ModelChanged;
    }
    
    /// <summary>   Event handler. Called by Items for collection changed events. </summary>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Notify collection changed event information. </param>
    private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs? e)
    {
        Debug.WriteLine($"CollectionChanged @{typeof(T)}");
        if (e != null)
        {
            if (e.OldItems != null)
                foreach (ISaveChangesAware item in e.OldItems)
                    item.ModelChanged -= Item_ModelChanged;

            if (e.NewItems == null) return;
            {
                foreach (ISaveChangesAware item in e.NewItems)
                    item.ModelChanged += Item_ModelChanged;
            }
        }

        var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        OnCollectionChanged(reset);
    }

    /// <summary>   Event handler. Called by Item for model changed events. </summary>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Event information. </param>
    private void Item_ModelChanged(object? sender, EventArgs e)
    {
        Debug.WriteLine($"ItemModelChanged @{typeof(T)}");
        var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        OnCollectionChanged(reset);
    }
}