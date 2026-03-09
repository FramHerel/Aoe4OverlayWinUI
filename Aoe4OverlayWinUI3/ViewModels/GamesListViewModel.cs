using System.Collections.ObjectModel;

using Aoe4OverlayWinUI3.Contracts.ViewModels;
using Aoe4OverlayWinUI3.Core.Contracts.Services;
using Aoe4OverlayWinUI3.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Aoe4OverlayWinUI3.ViewModels;

public partial class GamesListViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public GamesListViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
