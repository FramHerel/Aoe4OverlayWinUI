using Aoe4OverlayWinUI3.Contracts.Services;
using Aoe4OverlayWinUI3.ViewModels;

using Microsoft.UI.Xaml;

namespace Aoe4OverlayWinUI3.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigateTo(typeof(ProfileViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
