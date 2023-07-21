using System.Linq;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

public class ProjectService : PropertyChangedBase, IProjectService
{
    /// <summary>   The current design source. </summary>
    private IDataDesignSource? _currentDesignSource;

    /// <summary>   The current project. </summary>
    private DesignSchemaProject? _currentProject;

    /// <summary>   Constructor. </summary>
    /// <param name="projectFileService">   The project file service. </param>
    /// <param name="schemaService">        The schema service. </param>
    /// <param name="entityService">        The entity service. </param>
    /// <param name="propertyService">      The property service. </param>
    public ProjectService(IProjectFileService projectFileService, ISchemaService schemaService,
        IEntityService entityService, IPropertyService propertyService)
    {
        ProjectFileService = projectFileService;
        SchemaService = schemaService;
        EntityService = entityService;
        PropertyService = propertyService;
        Load();
        ProjectFileService.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(ProjectFileService.CurrentFile)) Load();
        };
    }

    /// <summary>   Gets the project file service. </summary>
    /// <value> The project file service. </value>
    public IProjectFileService ProjectFileService { get; }

    /// <summary>   Gets the schema service. </summary>
    /// <value> The schema service. </value>
    public ISchemaService SchemaService { get; }

    /// <summary>   Gets the entity service. </summary>
    /// <value> The entity service. </value>
    public IEntityService EntityService { get; }

    /// <summary>   Gets the property service. </summary>
    /// <value> The property service. </value>
    public IPropertyService PropertyService { get; }

    /// <summary>   Gets or sets the current project. </summary>
    /// <value> The current project. </value>
    public DesignSchemaProject? CurrentProject
    {
        get => _currentProject;
        private set => SetField(ref _currentProject, value);
    }

    /// <summary>   Gets or sets the current design source. </summary>
    /// <value> The current design source. </value>
    public IDataDesignSource? CurrentDesignSource
    {
        get => _currentDesignSource;
        private set => SetField(ref _currentDesignSource, value);
    }

    /// <summary>   Loads this object. </summary>
    private void Load()
    {
        if (ProjectFileService.CurrentFile != null)
        {
            CurrentDesignSource = new JsonDataDesignSource(ProjectFileService.CurrentFile);
            CurrentProject = CurrentDesignSource.Load();
            SchemaService.CurrentSchema = CurrentProject.Schemata.FirstOrDefault();
            EntityService.CurrentEntity = SchemaService.CurrentSchema?.Entities.FirstOrDefault();
            PropertyService.CurrentProperty = EntityService.CurrentEntity?.Properties.FirstOrDefault();
        }
        else
        {
            CurrentDesignSource = null;
            CurrentProject = null;
            SchemaService.CurrentSchema = null;
            EntityService.CurrentEntity = null;
            PropertyService.CurrentProperty = null;
        }
    }
}