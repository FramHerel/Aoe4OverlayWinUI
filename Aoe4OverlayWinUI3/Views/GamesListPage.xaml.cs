using Aoe4OverlayWinUI3.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Aoe4OverlayWinUI3.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class GamesListPage : Page
{
    public GamesListViewModel ViewModel
    {
        get;
    }

    public GamesListPage()
    {
        ViewModel = App.GetService<GamesListViewModel>();
        InitializeComponent();
    }
}
