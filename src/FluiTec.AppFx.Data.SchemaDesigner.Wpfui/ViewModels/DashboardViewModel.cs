using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;

/// <summary>   A ViewModel for the dashboard. </summary>
public partial class DashboardViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty] private Visibility _projectVisible = Visibility.Hidden;

    /// <summary>   Gets the select file service. </summary>
    /// <value> The select file service. </value>
    public ISelectFileService SelectFileService { get; }

    /// <summary>   Gets the project file service. </summary>
    /// <value> The project file service. </value>
    public IProjectFileService ProjectFileService { get; }

    /// <summary>   Gets the navigation service. </summary>
    /// <value> The navigation service. </value>
    public INavigationService NavigationService { get; }

    /// <summary>   Gets the create file service. </summary>
    /// <value> The create file service. </value>
    public ICreateFileService CreateFileService { get; }

    /// <summary>   Gets the project service. </summary>
    /// <value> The project service. </value>
    public IProjectService ProjectService { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="selectFileService">    The select file service. </param>
    /// <param name="projectFileService">   The project file service. </param>
    /// <param name="navigationService">    The navigation service. </param>
    /// <param name="createFileService">    The create file service. </param>
    /// <param name="projectService">       The project service. </param>
    public DashboardViewModel(ISelectFileService selectFileService, IProjectFileService projectFileService, INavigationService navigationService, ICreateFileService createFileService, IProjectService projectService)
    {
        SelectFileService = selectFileService;
        ProjectFileService = projectFileService;
        NavigationService = navigationService;
        CreateFileService = createFileService;
        ProjectService = projectService;
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {
        
    }

    [RelayCommand]
    private void OnCreateProject()
    {
        if (!CreateFileService.CreateFile(out var fileName)) return;

        var source = new JsonDataDesignSource(fileName);
        var newProject = source.Load();
        source.Save(newProject);
        
        ProjectVisible = Visibility.Visible;
        ProjectFileService.CurrentFile = fileName;
        NavigationService.Navigate(typeof(ProjectPage));
    }

    [RelayCommand]
    private void OnOpenProject()
    {
        if (!SelectFileService.SelectFile(out var fileName)) return;

        ProjectVisible = Visibility.Visible;
        ProjectFileService.CurrentFile = fileName;
        NavigationService.Navigate(typeof(ProjectPage));
    }
}