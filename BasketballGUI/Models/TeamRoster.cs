using System;
using System.Collections.Generic;


public partial class TeamRoster
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int TeamID { get; set; }

    public int PlayerID { get; set; }

    public int PlayerTeamID { get; set; }

    public string FullName => $"{Fname}{Lname}";
}