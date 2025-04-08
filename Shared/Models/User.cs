using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    public string FullName { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string Email { get; set; } = null!;

    public bool? EmailConfirmed { get; set; }

    public string? Phone { get; set; }

    public bool? PhoneConfirmed { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Bio { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? UpToAuthor { get; set; }

    public int? ReportCount { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Address { get; set; }

    public string? IdentificationNumber { get; set; }
    [JsonIgnore]
    public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; } = new List<FriendRequest>();
    [JsonIgnore]
    public virtual ICollection<FriendRequest> FriendRequestSenders { get; set; } = new List<FriendRequest>();
    [JsonIgnore]
    public virtual Role? Role { get; set; } = null!;
}
