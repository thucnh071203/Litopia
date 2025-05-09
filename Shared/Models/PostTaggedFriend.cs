﻿using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class PostTaggedFriend
{
    public Guid PostId { get; set; }

    public Guid UserId { get; set; }

    public virtual Post Post { get; set; } = null!;
}
