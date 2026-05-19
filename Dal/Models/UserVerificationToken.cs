using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class UserVerificationToken
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public bool IsVerified { get; set; }

    public DateTime ExpirationTime { get; set; }

    public virtual CustomersTbl User { get; set; } = null!;
}
