using System;
using System.Windows;
using System.Windows.Controls;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Windows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : INavigationWindow
{
    /// <summary>   Constructor. </summary>
    /// <param name="viewModel">            The view model. </param>
    /// <param name="pageService">          The page service. </param>
    /// <param name="navigationService">    The navigation service. </param>
    /// <param name="snackbarService">      The snack bar service. </param>
    /// <param name="dialogService">        The dialog service. </param>
    public MainWindow(MainWindowViewModel viewModel, IPageService pageService, INavigationService navigationService,
        ISnackbarService snackbarService, IDialogService dialogService)
    {
        ViewModel = viewModel;
        DialogService = dialogService;
        DataContext = this;

        InitializeComponent();
        SetPageService(pageService);

        navigationService.SetNavigationControl(RootNavigation);
        snackbarService.SetSnackbarControl(Snackbar);
        DialogService.SetDialogControl(ContentDialog);
    }

    public MainWindowViewModel ViewModel { get; }
    public IDialogService DialogService { get; }

    /// <summary>
    ///     Raises the closed event.
    /// </summary>
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Make sure that closing this window will begin the process of closing the application.
        Application.Current.Shutdown();
    }

    #region INavigationWindow methods

    public Frame GetFrame()
    {
        return RootFrame;
    }

    public INavigation GetNavigation()
    {
        return RootNavigation;
    }

    public bool Navigate(Type pageType)
    {
        return RootNavigation.Navigate(pageType);
    }

    public void SetPageService(IPageService pageService)
    {
        RootNavigation.PageService = pageService;
    }

    public void ShowWindow()
    {
        Show();
    }

    public void CloseWindow()
    {
        Close();
    }

    #endregion INavigationWindow methods
}