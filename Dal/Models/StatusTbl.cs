using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class StatusTbl
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
