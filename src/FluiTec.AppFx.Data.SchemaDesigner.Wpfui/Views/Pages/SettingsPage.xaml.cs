using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;

/// <summary>
///     Interaction logic for SettingsPage.xaml
/// </summary>
public partial class SettingsPage : INavigableView<SettingsViewModel>
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public SettingsViewModel ViewModel { get; }
}