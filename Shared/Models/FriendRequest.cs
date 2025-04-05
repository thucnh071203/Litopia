using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class FriendRequest
{
    public Guid RequestId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string? Status { get; set; }

    public DateTime? RequestDate { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
