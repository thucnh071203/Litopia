using System;
using System.Collections.Generic;

namespace BookService.Models;

public partial class BookLike
{
    public Guid BookLikeId { get; set; }

    public Guid UserId { get; set; }

    public Guid BookId { get; set; }

    public DateTime? LikedDate { get; set; }

    public virtual Book Book { get; set; } = null!;
}
