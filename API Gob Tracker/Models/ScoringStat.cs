using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class ScoringStat
{
    public decimal? Total1PtsMade { get; set; }

    public decimal? Total2PtsMade { get; set; }

    public decimal? Total3PtsMade { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }
}
