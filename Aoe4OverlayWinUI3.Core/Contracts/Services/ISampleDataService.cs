using Aoe4OverlayWinUI3.Core.Models;

namespace Aoe4OverlayWinUI3.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetGridDataAsync();
}
