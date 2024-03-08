using System;
using System.Collections.Generic;



public partial class AllRawStat
{
    public string Lname { get; set; } = null!;

    public string Fname { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Abrv { get; set; } = null!;

    public decimal StatValue { get; set; }

    public int GameId { get; set; }

    public int PlayerTeamId { get; set; }

    public int StatTypeId { get; set; }
    public int Id { get; internal set; }
}
