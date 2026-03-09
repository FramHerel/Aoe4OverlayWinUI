namespace Aoe4OverlayWinUI3.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
