using System;
using System.Collections.Generic;


public partial class StatType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Abrv { get; set; } = null!;

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();
}
