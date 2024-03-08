using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class Game
{
    public int Id { get; set; }

    public int Team1Id { get; set; }

    public int Team2Id { get; set; }

    public DateTimeOffset DateTimeId { get; set; }

    //public virtual ICollection<SeasonStat> Stats { get; set; } = new List<SeasonStat>();

    //public virtual Team Team1 { get; set; } = null!;

    //public virtual Team Team2 { get; set; } = null!;
}
