using System;
using System.Collections.Generic;


public partial class Schedule
{
    public string HomeTeam { get; set; } = null!;

    public string AwayTeam { get; set; } = null!;

    public DateTimeOffset DateTimeId { get; set; }
    public int Id { get;  set; }
}
