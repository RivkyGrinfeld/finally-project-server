using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class PointsTestTbl
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public int PropertyId { get; set; }

    public int GradeProperty { get; set; }

    public virtual PropertiesTbl Property { get; set; } = null!;

    public virtual TestsTbl Test { get; set; } = null!;
}
