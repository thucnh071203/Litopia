using System;
using System.Collections.Generic;

namespace Shared.Models;

public partial class Rating
{
    public Guid RatingId { get; set; }

    public Guid BookId { get; set; }

    public Guid UserId { get; set; }

    public int Score { get; set; }

    public string? Comment { get; set; }

    public DateTime? RatingDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Book Book { get; set; } = null!;
}
