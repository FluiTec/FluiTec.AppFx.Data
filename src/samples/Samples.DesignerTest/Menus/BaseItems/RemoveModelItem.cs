using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;
using Spectre.Console;

namespace Samples.DesignerTest.Menus.BaseItems;

/// <summary>   A remove model item. </summary>
/// <typeparam name="TModel">   Type of the model. </typeparam>
public class RemoveModelItem<TModel> : CommandConsoleItem
{
    /// <summary>   Constructor. </summary>
    /// <param name="models">   The models. </param>
    /// <param name="item">     The item. </param>
    public RemoveModelItem(IList<TModel> models, TModel item) : base("Remove")
    {
        Models = models;
        Item = item;
    }

    /// <summary>   Gets the models. </summary>
    /// <value> The models. </value>
    public IList<TModel> Models { get; }

    /// <summary>   Gets the item. </summary>
    /// <value> The item. </value>
    public TModel Item { get; }

    /// <summary>   Displays the given parent. </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IInteractiveConsoleItem? parent)
    {
        if (AnsiConsole.Confirm($"Are you sure you want to delete '{Item}'?")) Models.Remove(Item);

        parent!.Display(parent.Parent);
    }
}