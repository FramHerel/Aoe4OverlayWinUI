using Aoe4OverlayWinUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoe4OverlayWinUI.ViewModels
{
    public class GamesViewModel
    {
        public ObservableCollection<GameData> Items { get; } = new ObservableCollection<GameData>
        {
            new ()
            {
                Team1 = "Team A",
                Team2 = "Team B",
                Map1 = "Map 1",
                StartedTime = "2024-06-01 12:00",
                Mode = "Ranked",
                Result = "Win",
                DeltaRating = "+25"
            },
            new ()
            {
                Team1 = "Team C",
                Team2 = "Team D",
                Map1 = "Map 2",
                StartedTime = "2024-06-02 14:30",
                Mode = "Unranked",
                Result = "Loss",
                DeltaRating = "-15"
            }
        };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string? name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
