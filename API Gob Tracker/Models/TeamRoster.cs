using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class TeamRoster
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int TeamId { get; set; }

    public int PlayerId { get; set; }

    public int PlayerTeamID { get; set; }
}
