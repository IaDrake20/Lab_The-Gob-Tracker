using System;
using System.Collections.Generic;

namespace API_Gob_Tracker;

public partial class Game
{
    public int Id { get; set; }

    public int Team1Id { get; set; }

    public int Team2Id { get; set; }

    public DateTimeOffset DateTimeId { get; set; }

    /*public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual Team Team1 { get; set; } = null!;

    public virtual Team Team2 { get; set; } = null!;*/
}
