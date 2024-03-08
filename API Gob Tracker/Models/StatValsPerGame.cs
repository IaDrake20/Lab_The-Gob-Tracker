using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class StatValsPerGame
{
    public string StatName { get; set; } = null!;

    public decimal? StatValue { get; set; }

    public int? TeamId { get; set; }

    public int? GameId { get; set; }
}
