using System;
using System.Collections.Generic;

namespace BookService.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public string? Author { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal? AverageRating { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BookLike> BookLikes { get; set; } = new List<BookLike>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
