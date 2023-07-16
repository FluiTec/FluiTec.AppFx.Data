using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;

namespace Samples.DesignerTest.Menus.BaseItems;

/// <summary>   A new model item. </summary>
/// <typeparam name="TModel">   Type of the model. </typeparam>
public class NewModelItem<TModel> : CommandConsoleItem
{
    /// <summary>   (Immutable) the new model function. </summary>
    private readonly Func<TModel> _newModelFunc;

    /// <summary>   Gets the models. </summary>
    /// <value> The models. </value>
    public IList<TModel> Models { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="models">       The models. </param>
    /// <param name="newModelFunc"> The new model function. </param>
    public NewModelItem(IList<TModel> models, Func<TModel> newModelFunc) : base("Create new")
    {
        _newModelFunc = newModelFunc;
        Models = models;
    }

    /// <summary>   Displays the given parent. </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IInteractiveConsoleItem? parent)
    {
        var model = _newModelFunc();
        Models.Add(model);
        parent!.Display(parent.Parent);
    }
}