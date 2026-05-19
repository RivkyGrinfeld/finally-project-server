using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class TestsTbl
{
    public int TestId { get; set; }

    public string CustId { get; set; } = null!;

    public string Grade { get; set; } = null!;

    public virtual CustomersTbl Cust { get; set; } = null!;

    public virtual ICollection<PointsTestTbl> PointsTestTbls { get; set; } = new List<PointsTestTbl>();
}
