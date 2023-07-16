using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using Samples.DesignerTest.Menus.BaseItems;
using Spectre.Console;

namespace Samples.DesignerTest.Menus;

/// <summary>   A project schema item. </summary>
public class ProjectSchemaItem : ListEditItem<DesignSchema>
{
    /// <summary>   Gets the project. </summary>
    /// <value> The project. </value>
    public DesignSchemaProject Project { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="project">  The project. </param>
    public ProjectSchemaItem(DesignSchemaProject project) : base("ProjectSchema", nameof(project.Schemata), project.Schemata, NewModelFunc)
    {
        Project = project;
    }

    /// <summary>   Creates a new model function. </summary>
    /// <returns>   A DesignSchema. </returns>
    private static DesignSchema NewModelFunc()
    {
        var name = AnsiConsole.Ask<string>("Please enter name of the schema:");
        return new DesignSchema { Name = name };
    }

    /// <summary>   Converts the given model. </summary>
    /// <param name="model">    The model. </param>
    /// <returns>   An IInteractiveConsoleItem. </returns>
    protected override IInteractiveConsoleItem Convert(DesignSchema model)
    {
        return new SchemaItem(model);
    }
}