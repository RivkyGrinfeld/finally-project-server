using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class PostsTbl
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int PositionId { get; set; }

    public bool IsAvailble { get; set; }

    public string City { get; set; } = null!;

    public long? Salary { get; set; }

    public DateTime Date { get; set; }

    public bool IsConfirmed { get; set; }

    public int MaxCadidated { get; set; }

    public virtual ICollection<ApplyTbl> ApplyTbls { get; set; } = new List<ApplyTbl>();

    public virtual CompaniesTbl Company { get; set; } = null!;

    public virtual PositionsTbl Position { get; set; } = null!;

    public virtual ICollection<RequestsTbl> RequestsTbls { get; set; } = new List<RequestsTbl>();
}
