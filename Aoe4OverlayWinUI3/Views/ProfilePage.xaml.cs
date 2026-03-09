using Aoe4OverlayWinUI3.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Aoe4OverlayWinUI3.Views;

public sealed partial class ProfilePage : Page
{
    public ProfileViewModel ViewModel
    {
        get;
    }

    public ProfilePage()
    {
        ViewModel = App.GetService<ProfileViewModel>();
        InitializeComponent();
    }
}
