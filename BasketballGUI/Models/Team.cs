﻿using System;
using System.Collections.Generic;


public partial class Team
{
    public int Id { get; set; }

    public int Ranking { get; set; }

    public string Name { get; set; } = null!;

    /*public virtual ICollection<Game> GameTeam1s { get; set; } = new List<Game>();

    public virtual ICollection<Game> GameTeam2s { get; set; } = new List<Game>();

    public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();*/
}
