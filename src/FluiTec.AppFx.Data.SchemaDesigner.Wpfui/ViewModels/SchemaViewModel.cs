using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

/// <summary>   A ViewModel for the schema. </summary>
public partial class SchemaViewModel : ObservableObject, INavigationAware
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
    /// <param name="navigationService">    The navigation service. </param>
    /// <param name="snackbarService">      The snack bar service. </param>
    /// <param name="confirmService">       The confirm service. </param>
    public SchemaViewModel(ISchemaService schemaService, IProjectService projectService, IEntityService entityService, INavigationService navigationService, ISnackbarService snackbarService, IConfirmService confirmService)
    {
        SchemaService = schemaService;
        ProjectService = projectService;
        EntityService = entityService;
        NavigationService = navigationService;
        SnackbarService = snackbarService;
        ConfirmService = confirmService;
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
    }

    /// <summary>   Executes the 'create entity' action. </summary>
    [RelayCommand]
    public void OnCreateEntity()
    {
        SchemaService.CurrentSchema!.Entities.Add(new DesignEntity { Name = "EntityName", Properties = new ObservableCollectionWithItemNotify<DesignProperty>()});
    }

    /// <summary>   Executes the 'save schema' action. </summary>
    [RelayCommand]
    public void OnSaveSchema()
    {
        ProjectService.CurrentDesignSource!.Save(ProjectService.CurrentProject!);
        SnackbarService.Show("Project saved", "The project was successfully saved.", SymbolRegular.Save20,
            ControlAppearance.Success);
    }

    /// <summary>   Executes the 'edit entity' action. </summary>
    /// <param name="entity">   The entity. </param>
    [RelayCommand]
    public void OnEditEntity(DesignEntity entity)
    {
        EntityService.CurrentEntity = entity;
        NavigationService.Navigate(typeof(EntityPage));
    }

    /// <summary>   Executes the 'delete entity' action. </summary>
    /// <param name="entity">   The entity. </param>
    [RelayCommand]
    public void OnDeleteEntity(DesignEntity entity)
    {
        if (ConfirmService.Confirm("Delete entity?", $"Delete entity '{entity.Name}'?"))
            SchemaService.CurrentSchema!.Entities.Remove(entity);
    }
}