using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class RequestsTbl
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int PropertyId { get; set; }

    public int MinGradeProperty { get; set; }

    public virtual PostsTbl Post { get; set; } = null!;

    public virtual PropertiesTbl Property { get; set; } = null!;
}
