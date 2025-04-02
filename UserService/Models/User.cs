using System;
using System.Collections.Generic;

namespace UserService.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid? RoleId { get; set; }

    public string? FullName { get; set; }

    public string? Avatar { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string? Phone { get; set; }

    public bool? PhoneConfirmed { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Bio { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? UpToAuthor { get; set; }

    public int? ReportCount { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; } = new List<FriendRequest>();

    public virtual ICollection<FriendRequest> FriendRequestSenders { get; set; } = new List<FriendRequest>();

    public virtual Role? Role { get; set; }
}
