using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Group
{
    public Guid GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public Guid CreatorId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsApprovalRequired { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsPublic { get; set; }

    public virtual ICollection<GroupBook> GroupBooks { get; set; } = new List<GroupBook>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<GroupPendingMember> GroupPendingMembers { get; set; } = new List<GroupPendingMember>();
}
