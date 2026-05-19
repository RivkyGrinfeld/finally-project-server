using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class ManagersTbl
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int UserId { get; set; } 

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
