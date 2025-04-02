using System;
using System.Collections.Generic;

namespace ChatService.Models;

public partial class MessageMedium
{
    public Guid MessageId { get; set; }

    public string MediaUrl { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
