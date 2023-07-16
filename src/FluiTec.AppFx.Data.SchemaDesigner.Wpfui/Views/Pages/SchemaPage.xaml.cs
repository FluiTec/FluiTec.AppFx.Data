using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;

/// <summary>
///     Interaction logic for DataView.xaml
/// </summary>
public partial class SchemaPage : INavigableView<SchemaViewModel>
{
    public SchemaPage(SchemaViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public SchemaViewModel ViewModel { get; }
}