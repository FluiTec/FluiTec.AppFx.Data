using System.Collections.ObjectModel;

namespace Samples.DesignerTest.Menus.BaseItems;

/// <summary>   A property editing list edit item. </summary>
/// <typeparam name="TModel">   Type of the model. </typeparam>
public abstract class PropertyEditingListEditItem<TModel> : ListEditItem<TModel>
{
    protected PropertyEditingListEditItem(string name, string listName, ObservableCollection<TModel> models,
        Func<TModel> newModelFunc) : base(name, listName, models, newModelFunc)
    {
    }

    protected override void AddItems()
    {
        base.AddItems();
    }
}