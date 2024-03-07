using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class StatsPlayer
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Abrv { get; set; } = null!;

    public decimal StatValue { get; set; }
}
