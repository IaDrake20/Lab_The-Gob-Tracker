using System;
using System.Collections.Generic;

namespace API_Gob_Tracker;

public partial class Player
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();
}
