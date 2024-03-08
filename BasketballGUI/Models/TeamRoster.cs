using System;
using System.Collections.Generic;


public partial class TeamRoster
{
    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Name { get; set; } = null!;
    public int Id { get; internal set; }

    public string FullName => $"{Fname} {Lname}";
}
