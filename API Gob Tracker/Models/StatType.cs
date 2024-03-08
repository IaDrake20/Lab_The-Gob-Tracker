using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class StatType
{
    public int Id { get; internal set; }

    public string Name { get; set; } = null!;

    public string Abrv { get; set; } = null!;

    //public virtual ICollection<SeasonStat> Stats { get; set; } = new List<SeasonStat>();
}
