using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

/// <summary>   A ViewModel for the entity. </summary>
public partial class EntityViewModel : ObservableObject, INavigationAware
{
    /// <summary>   Gets the schema service. </summary>
    /// <value> The schema service. </value>
    public ISchemaService SchemaService { get; }

    /// <summary>   Gets the project service. </summary>
    /// <value> The project service. </value>
    public IProjectService ProjectService { get; }

    /// <summary>   Gets the entity service. </summary>
    /// <value> The entity service. </value>
    public IEntityService EntityService { get; }

    /// <summary>   Gets the property service. </summary>
    /// <value> The property service. </value>
    public IPropertyService PropertyService { get; }

    /// <summary>   Gets the navigation service. </summary>
    /// <value> The navigation service. </value>
    public INavigationService NavigationService { get; }

    /// <summary>   Gets the snack bar service. </summary>
    /// <value> The snack bar service. </value>
    public ISnackbarService SnackbarService { get; }

    /// <summary>   Gets the confirm service. </summary>
    /// <value> The confirm service. </value>
    public IConfirmService ConfirmService { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="schemaService">        The schema service. </param>
    /// <param name="projectService">       The project service. </param>
    /// <param name="entityService">        The entity service. </param>
    /// <param name="propertyService">      The property service. </param>
    /// <param name="navigationService">    The navigation service. </param>
    /// <param name="snackbarService">      The snack bar service. </param>
    /// <param name="confirmService">       The confirm service. </param>
    public EntityViewModel(ISchemaService schemaService, IProjectService projectService, IEntityService entityService, IPropertyService propertyService, INavigationService navigationService, ISnackbarService snackbarService, IConfirmService confirmService)
    {
        SchemaService = schemaService;
        ProjectService = projectService;
        EntityService = entityService;
        PropertyService = propertyService;
        NavigationService = navigationService;
        SnackbarService = snackbarService;
        ConfirmService = confirmService;
    }

    /// <summary>   Executes the 'create property' action. </summary>
    [RelayCommand]
    public void OnCreateProperty()
    {
        EntityService.CurrentEntity!.Properties.Add(new DesignProperty { Name = "PropertyName", Type = typeof(string)});
    }

    /// <summary>   Executes the 'save schema' action. </summary>
    [RelayCommand]
    public void OnSaveSchema()
    {
        ProjectService.CurrentDesignSource!.Save(ProjectService.CurrentProject!);
        SnackbarService.Show("Project saved", "The project was successfully saved.", SymbolRegular.Save20,
            ControlAppearance.Success);
    }

    /// <summary>   Executes the 'edit property' action. </summary>
    /// <param name="property"> The property. </param>
    [RelayCommand]
    public void OnEditProperty(DesignProperty property)
    {
        PropertyService.CurrentProperty = property;
        NavigationService.Navigate(typeof(PropertyPage));
    }

    /// <summary>   Executes the 'delete property' action. </summary>
    /// <param name="property"> The property. </param>
    [RelayCommand]
    public void OnDeleteProperty(DesignProperty property)
    {
        if (ConfirmService.Confirm("Delete property?", $"Delete property '{property.Name}'?"))
            EntityService.CurrentEntity!.Properties.Remove(property);
    }

    /// <summary>   Method triggered when the class is navigated. </summary>
    public void OnNavigatedTo()
    {
    }

    /// <summary>   Method triggered when the navigation leaves the current class. </summary>
    public void OnNavigatedFrom()
    {
    }
}