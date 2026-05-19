using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class CompaniesTbl
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<PostsTbl> PostsTbls { get; set; } = new List<PostsTbl>();

    public virtual User User { get; set; } = null!;
}
