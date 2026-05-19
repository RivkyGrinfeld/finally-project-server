using System;
using System.Collections.Generic;

namespace Pl_Web_Api.Models;

public partial class City
{
    public int Id { get; set; }

    public string? CityName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
