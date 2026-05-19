using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class PositionsTbl
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string Description { get; set; } = null!;

    public virtual BranchesTbl Branch { get; set; } = null!;

    public virtual ICollection<PostsTbl> PostsTbls { get; set; } = new List<PostsTbl>();
}
