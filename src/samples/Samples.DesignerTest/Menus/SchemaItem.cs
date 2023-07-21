using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using Samples.DesignerTest.Menus.BaseItems;
using Spectre.Console;

namespace Samples.DesignerTest.Menus;

/// <summary>   A schema item. </summary>
public class SchemaItem : PropertyEditingListEditItem<DesignEntity>
{
    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    public SchemaItem(DesignSchema schema) : base(schema.Name, nameof(schema.Entities), schema.Entities, NewModelFunc)
    {
        Schema = schema;
    }

    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public DesignSchema Schema { get; }

    /// <summary>   Creates a new model function. </summary>
    /// <returns>   A DesignEntity. </returns>
    private static DesignEntity NewModelFunc()
    {
        var name = AnsiConsole.Ask<string>("Please enter name of the entity:");
        return new DesignEntity { Name = name };
    }

    /// <summary>   Converts the given model. </summary>
    /// <param name="model">    The model. </param>
    /// <returns>   An IInteractiveConsoleItem. </returns>
    protected override IInteractiveConsoleItem Convert(DesignEntity model)
    {
        return new CommandConsoleItem(model.Name);
    }
}