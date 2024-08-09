using System;
using System.Collections.Generic;

namespace EcomerceMVC.Data;

public partial class UserRole
{
    public string? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
