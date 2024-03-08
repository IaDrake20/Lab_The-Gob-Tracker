using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class ScoringStat
{
    public int GameID { get; set; }
    public int TeamID { get; set; }
    public int TotalPtsMade { get; set; }
}
