using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Image
{
    public Guid ImageId { get; set; }

    public string Url { get; set; } = null!;

    public Guid PostId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
