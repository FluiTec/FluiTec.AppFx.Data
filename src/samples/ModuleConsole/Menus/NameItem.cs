using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;
using Spectre.Console;

namespace ModuleConsole.Menus;

/// <summary>   A name item. </summary>
public class NameItem : CommandConsoleItem
{
    /// <summary>   Constructor. </summary>
    /// <param name="name"> The name. </param>
    public NameItem(string name) : base(name)
    {
    }

    /// <summary>   Displays this. </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IInteractiveConsoleItem? parent)
    {
        if (AnsiConsole.Confirm($"You sure you wanna pick {Name}?"))
        {
            // do your thing
        }

        base.Display(parent);
    }
}