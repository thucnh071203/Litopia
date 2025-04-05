using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string? Type { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool? IsRead { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<MessageMedium> MessageMedia { get; set; } = new List<MessageMedium>();
}
