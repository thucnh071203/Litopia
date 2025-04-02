using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Comment
{
    public Guid CommentId { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
