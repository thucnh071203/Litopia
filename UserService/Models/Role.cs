using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
