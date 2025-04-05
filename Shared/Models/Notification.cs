using System;
using System.Collections.Generic;

namespace NotificationService.Models;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ReferenceId { get; set; }

    public string? Content { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual NotificationCategory? Category { get; set; }
}
