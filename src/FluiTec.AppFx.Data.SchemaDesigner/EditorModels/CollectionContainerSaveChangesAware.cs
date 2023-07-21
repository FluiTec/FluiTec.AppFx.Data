using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A collection save changes aware. </summary>
public class CollectionSaveChangesAware : SaveChangesAware
{
    /// <summary>   Sets a collection. </summary>
    /// <typeparam name="TCollectionItem">  Type of the collection item. </typeparam>
    /// <param name="collection">   [in,out] The collection. </param>
    /// <param name="value">        The value. </param>
    /// <param name="propertyName"> Name of the property. </param>
    protected void SetCollection<TCollectionItem>(ref ObservableCollectionWithItemNotify<TCollectionItem>? collection,
        ObservableCollectionWithItemNotify<TCollectionItem> value, [CallerMemberName] string? propertyName = null)
        where TCollectionItem : ISaveChangesAware
    {
        if (collection != null)
        {
            collection.CollectionChanged -= OnValueOnCollectionChanged;
            Debug.WriteLine($"unregister @{propertyName}");
        }

        SetField(ref collection!, value, propertyName);
        collection.CollectionChanged += OnValueOnCollectionChanged;
        Debug.WriteLine($"register @{propertyName}");
    }

    /// <summary>   Raises the notify collection changed event. </summary>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="args">     Event information to send to registered event handlers. </param>
    private void OnValueOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        OnModelChanged();
    }
}