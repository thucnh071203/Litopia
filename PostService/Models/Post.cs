using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Post
{
    public Guid PostId { get; set; }

    public Guid? AuthorId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<PostTaggedFriend> PostTaggedFriends { get; set; } = new List<PostTaggedFriend>();

    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
}
