using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Hashtag
{
    public Guid HashtagId { get; set; }

    public string Name { get; set; } = null!;

    public int? UsageCount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
