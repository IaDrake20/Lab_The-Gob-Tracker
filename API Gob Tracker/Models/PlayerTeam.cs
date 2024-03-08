using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class PlayerTeam
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int TeamId { get; set; }

    /*public virtual Player Player { get; set; } = null!;

    public virtual ICollection<SeasonStat> Stats { get; set; } = new List<SeasonStat>();

    public virtual Team Team { get; set; } = null!;*/
}
