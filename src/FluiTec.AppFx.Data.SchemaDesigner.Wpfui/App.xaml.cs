using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Options;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>   (Immutable) the host. </summary>
    private static readonly IHost Host = Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(
            c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            // App Host
            services.AddHostedService<ApplicationHostService>();

            // Services
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<ITaskBarService, TaskBarService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ISelectFileService, WindowsSelectFileService>();
            services.AddSingleton<ICreateFileService, WindowsCreateFileService>();
            services.AddSingleton<IProjectFileService, ProjectFileService>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ISnackbarService, SnackbarService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IConfirmService, WindowsConfirmService>();
            services.AddSingleton<ISchemaService, SchemaService>();
            services.AddSingleton<IEntityService, EntityService>();
            services.AddSingleton<IPropertyService, PropertyService>();

            // Main window with navigation
            services.AddScoped<INavigationWindow, MainWindow>();
            services.AddScoped<MainWindowViewModel>();

            // Views 
            services.AddScoped<DashboardPage>();
            services.AddScoped<ProjectPage>();
            services.AddScoped<SettingsPage>();
            services.AddScoped<SchemaPage>();
            services.AddScoped<EntityPage>();
            services.AddScoped<PropertyPage>();

            // ViewModels
            services.AddScoped<DashboardViewModel>();
            services.AddScoped<ProjectViewModel>();
            services.AddScoped<SettingsViewModel>();
            services.AddScoped<SchemaViewModel>();
            services.AddScoped<EntityViewModel>();
            services.AddScoped<PropertyViewModel>();

            // Options
            services.Configure<ApplicationOptions>(context.Configuration.GetSection(nameof(ApplicationOptions)));
        }).Build();

    /// <summary>
    ///     Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null" />.</returns>
    public static T? GetService<T>()
        where T : class
    {
        return Host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    ///     Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        await Host.StartAsync();
    }

    /// <summary>
    ///     Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await Host.StopAsync();

        Host.Dispose();
    }

    /// <summary>
    ///     Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}