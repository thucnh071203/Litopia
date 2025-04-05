using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class GroupBookNote
{
    public Guid GroupBookId { get; set; }

    public Guid NoteId { get; set; }

    public string? Content { get; set; }

    public virtual GroupBook GroupBook { get; set; } = null!;
}
