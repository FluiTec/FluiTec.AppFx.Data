using FluiTec.AppFx.Console.Modularization;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.DefaultItems;
using FluiTec.AppFx.Console.Modularization.InteractiveItems.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using Spectre.Console;

namespace Samples.DesignerTest.Menus;

/// <summary>   A project item. </summary>
public class ProjectItem : CommandConsoleItem, IInteractiveConsoleMenu
{
    /// <summary>   Source for the. </summary>
    private IDataDesignSource? _source;

    /// <summary>   The project. </summary>
    private DesignSchemaProject? _project;

    /// <summary>   Default constructor. </summary>
    public ProjectItem() : base("Create / Open project")
    {
    }

    /// <summary>   Displays the given parent. </summary>
    /// <param name="parent">   The parent. </param>
    public override void Display(IInteractiveConsoleItem? parent)
    {
        var defPath = Path.Combine(Directory.GetCurrentDirectory(), "data.design");
        var path = defPath;
        while (!AnsiConsole.Confirm($"Create / Open project @ '{path}' ?"))
        {
            path = AnsiConsole.Ask<string>("Enter file name of the project:");
        }

        _source = new JsonDataDesignSource(path);
        _project = _source.Load();
        
        if (!File.Exists(path))
        {
            SaveDefault(_source, _project);
            _project = _source.Load();
        }

        _project.ModelChanged += Project_ModelChanged;
        var item = new ProjectSchemaItem(_project);
        item.Display(this);
    }

    private void Project_ModelChanged(object? sender, EventArgs e)
    {
        _source!.Save(_project!);
    }
    
    /// <summary>   Saves a default. </summary>
    /// <param name="dsource">   Source for the. </param>
    /// <param name="dproject">  The project. </param>
    private void SaveDefault(IDataDesignSource dsource, DesignSchemaProject dproject)
    {
        AddDummy(dproject);
        dsource.Save(dproject);
    }

    /// <summary>   Adds a dummy. </summary>
    /// <param name="dproject">  The project. </param>
    private void AddDummy(DesignSchemaProject dproject)
    {
        dproject.Schemata.Add(new DesignSchema
        {
            Name = "TestSchema",
            Entities = new ObservableCollectionWithItemNotify<DesignEntity>(new[] {
                new DesignEntity
                {
                    Name = "DummyEntity",
                    Properties = new ObservableCollectionWithItemNotify<DesignProperty>(new []
                    {
                        new DesignProperty
                        {
                            Name = "Id",
                            KeyOrder = 0,
                            Nullable = false,
                            Type = typeof(int)
                        },
                        new DesignProperty
                        {
                            Name = "Name",
                            KeyOrder = null,
                            Nullable = true,
                            Type = typeof(string)
                        }
                    })
                }
            })
        });
    }
}