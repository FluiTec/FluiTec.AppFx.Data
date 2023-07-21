using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using Wpf.Ui.Common;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

/// <summary>   A ViewModel for the property. </summary>
public partial class PropertyViewModel : ObservableObject, INavigationAware
{
    /// <summary>   (Immutable) list of types of the properties. </summary>
    [ObservableProperty] private ReadOnlyCollection<Type> _propertyTypes = new(new List<Type>(new[]
    {
        typeof(int),
        typeof(long),
        typeof(float),
        typeof(double),
        typeof(decimal),
        typeof(string),
        typeof(bool),
        typeof(DateTime),
        typeof(Guid)
    }));

    /// <summary>   Constructor. </summary>
    /// <param name="schemaService">    The schema service. </param>
    /// <param name="projectService">   The project service. </param>
    /// <param name="entityService">    The entity service. </param>
    /// <param name="propertyService">  The property service. </param>
    /// <param name="snackbarService">  The snack bar service. </param>
    /// <param name="confirmService">   The confirm service. </param>
    public PropertyViewModel(ISchemaService schemaService, IProjectService projectService, IEntityService entityService,
        IPropertyService propertyService, ISnackbarService snackbarService, IConfirmService confirmService)
    {
        SchemaService = schemaService;
        ProjectService = projectService;
        EntityService = entityService;
        PropertyService = propertyService;
        SnackbarService = snackbarService;
        ConfirmService = confirmService;
    }

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

    /// <summary>   Gets the snack bar service. </summary>
    /// <value> The snack bar service. </value>
    public ISnackbarService SnackbarService { get; }

    /// <summary>   Gets the confirm service. </summary>
    /// <value> The confirm service. </value>
    public IConfirmService ConfirmService { get; }

    public void OnNavigatedTo()
    {
    }

    /// <summary>   Method triggered when the navigation leaves the current class. </summary>
    public void OnNavigatedFrom()
    {
    }

    /// <summary>   Executes the 'save schema' action. </summary>
    [RelayCommand]
    public void OnSaveSchema()
    {
        ProjectService.CurrentDesignSource!.Save(ProjectService.CurrentProject!);
        SnackbarService.Show("Project saved", "The project was successfully saved.", SymbolRegular.Save20,
            ControlAppearance.Success);
    }
}