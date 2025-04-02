using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Reaction
{
    public Guid ReactionId { get; set; }

    public Guid? PostId { get; set; }

    public Guid? UserId { get; set; }

    public string? Reaction1 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Post? Post { get; set; }
}
