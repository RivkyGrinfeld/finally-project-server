using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class ApplyTbl
{
    public int Id { get; set; }

    public string CustId { get; set; } = null!;

    public int PostId { get; set; }

    public bool Confirmed { get; set; }

    public DateTime Date { get; set; }

    public virtual CustomersTbl Cust { get; set; } = null!;

    public virtual PostsTbl Post { get; set; } = null!;
}
