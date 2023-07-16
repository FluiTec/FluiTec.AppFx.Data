using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Views.Pages;

/// <summary>
///     Interaction logic for DataView.xaml
/// </summary>
public partial class EntityPage : INavigableView<EntityViewModel>
{
    /// <summary>   Constructor. </summary>
    /// <param name="viewModel">    The view model. </param>
    public EntityPage(EntityViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    /// <summary>
    ///     ViewModel used by the view. Optionally, it may implement <see cref="T:Wpf.Ui.Common.Interfaces.INavigationAware" />
    ///     and be navigated by <see cref="T:Wpf.Ui.Controls.Interfaces.INavigation" />.
    /// </summary>
    /// <value> The view model. </value>
    public EntityViewModel ViewModel { get; }
}