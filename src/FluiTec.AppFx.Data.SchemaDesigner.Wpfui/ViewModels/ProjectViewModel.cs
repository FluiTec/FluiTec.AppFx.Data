using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

/// <summary>   A ViewModel for the data. </summary>
public partial class ProjectViewModel : ObservableObject, INavigationAware
{
    /// <summary>   Constructor. </summary>
    /// <param name="projectService">       The project file service. </param>
    /// <param name="snackbarService">      The snack bar service. </param>
    /// <param name="dialogService">        The dialog service. </param>
    /// <param name="confirmService">       The confirm service. </param>
    /// <param name="schemaService">        The schema service. </param>
    /// <param name="navigationService">    The navigation service. </param>
    public ProjectViewModel(IProjectService projectService, ISnackbarService snackbarService,
        IDialogService dialogService, IConfirmService confirmService, ISchemaService schemaService,
        INavigationService navigationService)
    {
        ProjectService = projectService;
        SnackbarService = snackbarService;
        DialogService = dialogService;
        ConfirmService = confirmService;
        SchemaService = schemaService;
        NavigationService = navigationService;
    }

    /// <summary>   Gets the project service. </summary>
    /// <value> The project service. </value>
    public IProjectService ProjectService { get; }

    /// <summary>   Gets the snack bar service. </summary>
    /// <value> The snack bar service. </value>
    public ISnackbarService SnackbarService { get; }

    /// <summary>   Gets the dialog service. </summary>
    /// <value> The dialog service. </value>
    public IDialogService DialogService { get; }

    /// <summary>   Gets the confirm service. </summary>
    /// <value> The confirm service. </value>
    public IConfirmService ConfirmService { get; }

    /// <summary>   Gets the schema service. </summary>
    /// <value> The schema service. </value>
    public ISchemaService SchemaService { get; }

    /// <summary>   Gets the navigation service. </summary>
    /// <value> The navigation service. </value>
    public INavigationService NavigationService { get; }

    /// <summary>   Method triggered when the class is navigated. </summary>
    public void OnNavigatedTo()
    {
    }

    /// <summary>   Method triggered when the navigation leaves the current class. </summary>
    public void OnNavigatedFrom()
    {
    }

    /// <summary>   Executes the 'create schema' action. </summary>
    [RelayCommand]
    public void OnCreateSchema()
    {
        var schema = new DesignSchema
            { Name = "SchemaName", Entities = new ObservableCollectionWithItemNotify<DesignEntity>() };
        ProjectService.CurrentProject!.Schemata.Add(schema);
        SchemaService.CurrentSchema = schema;
        NavigationService.Navigate(typeof(SchemaPage));
    }

    /// <summary>   Executes the 'save schema' action. </summary>
    [RelayCommand]
    public void OnSaveSchema()
    {
        ProjectService.CurrentDesignSource!.Save(ProjectService.CurrentProject!);
        SnackbarService.Show("Project saved", "The project was successfully saved.", SymbolRegular.Save20,
            ControlAppearance.Success);
    }

    /// <summary>   Executes the 'edit schema' action. </summary>
    /// <param name="schema">   The schema. </param>
    [RelayCommand]
    public void OnEditSchema(DesignSchema schema)
    {
        SchemaService.CurrentSchema = schema;
        NavigationService.Navigate(typeof(SchemaPage));
    }

    /// <summary>   Executes the 'delete schema' action. </summary>
    /// <param name="schema">   The schema. </param>
    [RelayCommand]
    public void OnDeleteSchema(DesignSchema schema)
    {
        if (ConfirmService.Confirm("Delete schema?", $"Delete schema '{schema.Name}'?"))
            ProjectService.CurrentProject!.Schemata.Remove(schema);
    }
}