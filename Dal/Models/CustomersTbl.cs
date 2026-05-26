using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class CustomersTbl
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int BranchId { get; set; }

    public string Phone { get; set; } = null!;

    public string? FileName { get; set; }

    public string? Url { get; set; }

    public int UserId { get; set; }
   //public DateTime CreatedAt { get; set; }

    public virtual ICollection<ApplyTbl> ApplyTbls { get; set; } = new List<ApplyTbl>();

    public virtual BranchesTbl Branch { get; set; } = null!;

    public virtual ICollection<TestsTbl> TestsTbls { get; set; } = new List<TestsTbl>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserVerificationToken> UserVerificationTokens { get; set; } = new List<UserVerificationToken>();
}
