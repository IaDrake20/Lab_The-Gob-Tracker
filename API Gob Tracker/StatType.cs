using System;
using System.Collections.Generic;

namespace API_Gob_Tracker.Models;

public partial class StatType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

   /* public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();*/
}
