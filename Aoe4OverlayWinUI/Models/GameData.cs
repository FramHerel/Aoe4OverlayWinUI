using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoe4OverlayWinUI.Models
{
    public class GameData
    {
        public required string Team1 { get; set; }
        public required string Team2 { get; set; }
        public required string Map1 { get; set; }
        public required string StartedTime { get; set; }
        public required string Mode { get; set; }
        public required string Result { get; set; }
        public required string DeltaRating { get; set; }
    }
}
