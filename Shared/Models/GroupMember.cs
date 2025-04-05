using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class GroupMember
{
    public Guid GroupMemberId { get; set; }

    public Guid GroupId { get; set; }

    public Guid UserId { get; set; }

    public string Role { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? JoinedDate { get; set; }

    public virtual Group Group { get; set; } = null!;
}
