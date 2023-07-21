using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Converters;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Options;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using Microsoft.Extensions.Options;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string _applicationSubTitle = string.Empty;

    [ObservableProperty] private string _applicationTitle = string.Empty;

    [ObservableProperty] private string? _currentFile = string.Empty;
    private NavigationItem _entityItem = null!;

    private NavigationItem _homeItem = null!;
    private bool _isInitialized;

    [ObservableProperty] private ObservableCollection<INavigationControl> _navigationFooter = new();

    [ObservableProperty] private ObservableCollection<INavigationControl> _navigationItems = new();
    private NavigationItem _projectItem = null!;
    private NavigationItem _propertyItem = null!;
    private NavigationItem _schemaItem = null!;

    [ObservableProperty] private ObservableCollection<MenuItem> _trayMenuItems = new();

    /// <summary>   Constructor. </summary>
    /// <param name="navigationService">    The navigation service. </param>
    /// <param name="appOptions">           Options for controlling the application. </param>
    /// <param name="projectFileService">   The project file service. </param>
    /// <param name="projectService">       The project service. </param>
    /// <param name="schemaService">        The schema service. </param>
    /// <param name="entityService">        The entity service. </param>
    /// <param name="propertyService">      The property service. </param>
    public MainWindowViewModel(INavigationService navigationService, IOptions<ApplicationOptions> appOptions,
        IProjectFileService projectFileService, IProjectService projectService, ISchemaService schemaService,
        IEntityService entityService, IPropertyService propertyService)
    {
        NavigationService = navigationService;
        ProjectFileService = projectFileService;
        ProjectService = projectService;
        SchemaService = schemaService;
        EntityService = entityService;
        PropertyService = propertyService;
        ApplicationOptions = appOptions.Value;

        ApplicationTitle = ApplicationOptions.Title;
        ApplicationSubTitle = ApplicationOptions.SubTitle;

        ProjectFileService.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(ProjectFileService.CurrentFile))
                CurrentFile = ProjectFileService.CurrentFile;
        };

        if (!_isInitialized)
            InitializeViewModel();
    }

    /// <summary>   Gets the navigation service. </summary>
    /// <value> The navigation service. </value>
    public INavigationService NavigationService { get; }

    /// <summary>   Gets the project file service. </summary>
    /// <value> The project file service. </value>
    public IProjectFileService ProjectFileService { get; }

    /// <summary>   Gets the project service. </summary>
    /// <value> The project service. </value>
    public IProjectService ProjectService { get; }

    /// <summary>   Gets the schema service. </summary>
    /// <value> The schema service. </value>
    public ISchemaService SchemaService { get; }

    /// <summary>   Gets the entity service. </summary>
    /// <value> The entity service. </value>
    public IEntityService EntityService { get; }

    /// <summary>   Gets the property service. </summary>
    /// <value> The property service. </value>
    public IPropertyService PropertyService { get; }

    /// <summary>   Gets options for controlling the application. </summary>
    /// <value> Options that control the application. </value>
    public ApplicationOptions ApplicationOptions { get; }

    /// <summary>   Initializes the view model. </summary>
    private void InitializeViewModel()
    {
        InitializeNavItems();

        NavigationItems = new ObservableCollection<INavigationControl>
        {
            _homeItem, _projectItem, _schemaItem, _entityItem, _propertyItem
        };

        NavigationFooter = new ObservableCollection<INavigationControl>
        {
            new NavigationItem
            {
                Content = "Settings",
                PageTag = "settings",
                Icon = SymbolRegular.Settings24,
                PageType = typeof(SettingsPage)
            }
        };

        TrayMenuItems = new ObservableCollection<MenuItem>
        {
            new()
            {
                Header = "Home",
                Tag = "tray_home"
            }
        };

        _isInitialized = true;
    }

    /// <summary>   Initializes the navigation items. </summary>
    private void InitializeNavItems()
    {
        _homeItem = new NavigationItem
        {
            Content = "Home",
            PageTag = "dashboard",
            Icon = SymbolRegular.Home24,
            PageType = typeof(DashboardPage)
        };

        _projectItem = new NavigationItem
        {
            Content = "Project",
            PageTag = "project",
            Icon = SymbolRegular.DataHistogram24,
            PageType = typeof(ProjectPage),
            Visibility = ProjectService.CurrentProject != null ? Visibility.Visible : Visibility.Hidden
        };
        SetVisibilityToValueBinding(ProjectService, nameof(ProjectService.CurrentProject), _projectItem);

        _schemaItem = new NavigationItem
        {
            Content = "Schema",
            PageTag = "schema",
            Icon = SymbolRegular.Apps20,
            PageType = typeof(SchemaPage),
            Visibility = SchemaService.CurrentSchema != null ? Visibility.Visible : Visibility.Hidden
        };
        SetVisibilityToValueBinding(SchemaService, nameof(SchemaService.CurrentSchema), _schemaItem);

        _entityItem = new NavigationItem
        {
            Content = "Entity",
            PageTag = "entity",
            Icon = SymbolRegular.Table20,
            PageType = typeof(EntityPage),
            Visibility = EntityService.CurrentEntity != null ? Visibility.Visible : Visibility.Hidden
        };
        SetVisibilityToValueBinding(EntityService, nameof(EntityService.CurrentEntity), _entityItem);

        _propertyItem = new NavigationItem
        {
            Content = "Property",
            PageTag = "property",
            Icon = SymbolRegular.Tag20,
            PageType = typeof(PropertyPage),
            Visibility = PropertyService.CurrentProperty != null ? Visibility.Visible : Visibility.Hidden
        };
        SetVisibilityToValueBinding(PropertyService, nameof(PropertyService.CurrentProperty), _propertyItem);
    }

    /// <summary>   Sets visibility to value binding. </summary>
    /// <param name="source">       Source for the. </param>
    /// <param name="propertyName"> Name of the property. </param>
    /// <param name="element">      The element. </param>
    private void SetVisibilityToValueBinding(INotifyPropertyChanged source, string propertyName,
        FrameworkElement element)
    {
        var binding = new Binding(propertyName)
        {
            Source = source,
            Converter = new HasValueVisibilityConverter()
        };
        element.SetBinding(UIElement.VisibilityProperty, binding);
    }
}