using System.Collections.ObjectModel;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;

namespace Samples.DesignerTest.Menus.BaseItems;

/// <summary>   A list edit item. </summary>
/// <typeparam name="TModel">   Type of the model. </typeparam>
public abstract class ListEditItem<TModel> : SelectConsoleItem
{
    /// <summary>   (Immutable) the new model function. </summary>
    private readonly Func<TModel> _newModelFunc;

    /// <summary>   Constructor. </summary>
    /// <param name="name">         The name. </param>
    /// <param name="listName">     Name of the list. </param>
    /// <param name="models">       The models. </param>
    /// <param name="newModelFunc"> The new model function. </param>
    protected ListEditItem(string name, string listName, ObservableCollection<TModel> models,
        Func<TModel> newModelFunc) : base(name)
    {
        _newModelFunc = newModelFunc;
        ListName = listName;
        Models = models;
        Models.CollectionChanged += (sender, args) => AddItems();
        // ReSharper disable once VirtualMemberCallInConstructor
        AddItems();
    }

    /// <summary>   Gets the name of the list. </summary>
    /// <value> The name of the list. </value>
    public string ListName { get; }

    /// <summary>   Gets the models. </summary>
    /// <value> The models. </value>
    public ObservableCollection<TModel> Models { get; }

    /// <summary>   Gets the prompt title. </summary>
    /// <value> The prompt title. </value>
    public override string PromptTitle => $"[gold3]{Name}[/], {ListName}:";

    /// <summary>   Adds items. </summary>
    protected virtual void AddItems()
    {
        Items.Clear();

        Items.Add(new NewModelItem<TModel>(Models, _newModelFunc));

        foreach (var model in Models)
            // ReSharper disable once VirtualMemberCallInConstructor
            Items.Add(Convert(model));
    }

    /// <summary>   Converts the given model. </summary>
    /// <param name="model">    The model. </param>
    /// <returns>   An IInteractiveConsoleItem. </returns>
    protected abstract IInteractiveConsoleItem Convert(TModel model);

    public override void Display(IInteractiveConsoleItem? parent)
    {
        base.Display(parent);
    }
}