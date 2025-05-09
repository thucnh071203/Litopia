﻿using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class GroupPendingMember
{
    public Guid GroupId { get; set; }

    public Guid UserId { get; set; }

    public virtual Group Group { get; set; } = null!;
}
