using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class PropertiesTbl
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<PointsTestTbl> PointsTestTbls { get; set; } = new List<PointsTestTbl>();

    public virtual ICollection<QuestionsTbl> QuestionsTbls { get; set; } = new List<QuestionsTbl>();

    public virtual ICollection<RequestsTbl> RequestsTbls { get; set; } = new List<RequestsTbl>();
}
