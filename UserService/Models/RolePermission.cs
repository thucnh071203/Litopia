using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class RolePermission
{
    public Guid RoleId { get; set; }

    public string Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
