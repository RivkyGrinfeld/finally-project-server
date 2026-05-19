using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class BranchesTbl
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<CustomersTbl> CustomersTbls { get; set; } = new List<CustomersTbl>();

    public virtual ICollection<PositionsTbl> PositionsTbls { get; set; } = new List<PositionsTbl>();
}
