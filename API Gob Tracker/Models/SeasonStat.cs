using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class SeasonStat
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public decimal? Total2PtsMade { get; set; }

    public decimal? Total3PtsMade { get; set; }

    public int Id { get; set; }

    public int GameId { get; set; }
}
