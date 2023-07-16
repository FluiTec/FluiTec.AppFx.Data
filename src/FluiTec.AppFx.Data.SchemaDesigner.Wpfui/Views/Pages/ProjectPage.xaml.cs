using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;

/// <summary>
///     Interaction logic for DataView.xaml
/// </summary>
public partial class ProjectPage : INavigableView<ProjectViewModel>
{
    public ProjectPage(ProjectViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public ProjectViewModel ViewModel { get; }
}