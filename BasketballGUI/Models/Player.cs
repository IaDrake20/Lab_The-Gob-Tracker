using System;
using System.Collections.Generic;


public partial class Player
{
    public int Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    //public virtual ICollection<PlayerTeam> PlayerTeams { get; set; } = new List<PlayerTeam>();
}
