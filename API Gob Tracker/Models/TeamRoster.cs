using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class TeamRoster
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public int TeamNumber { get; set; }

    public string Name { get; set; } = null!;
}
