using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<CompaniesTbl> CompaniesTbls { get; set; } = new List<CompaniesTbl>();

    public virtual ICollection<CustomersTbl> CustomersTbls { get; set; } = new List<CustomersTbl>();

    public virtual ICollection<ManagersTbl> ManagersTbls { get; set; } = new List<ManagersTbl>();

    public virtual StatusTbl Status { get; set; } = null!;
}
