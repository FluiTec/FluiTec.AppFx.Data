using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;

/// <summary>
///     Interaction logic for DashboardPage.xaml
/// </summary>
public partial class DashboardPage : INavigableView<DashboardViewModel>
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public DashboardViewModel ViewModel { get; }
}