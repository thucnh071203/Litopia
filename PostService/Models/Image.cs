using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public string? Url { get; set; }

    public Guid? PostId { get; set; }

    public virtual Post? Post { get; set; }
}
