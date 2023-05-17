using FluiTec.AppFx.Console.Modularization;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;

namespace ModuleConsole.Menus;

/// <summary>   A name menu. </summary>
public class NameMenu : SelectConsoleItem, IInteractiveConsoleMenu
{
    /// <summary>   Default constructor. </summary>
    public NameMenu() : base("Name")
    {
        Items.Add(new NameItem("Achim"));
        Items.Add(new NameItem("Stefan"));
        Items.Add(new NameItem("Dagmar"));
        Items.Add(new NameItem("Walter"));
    }
}