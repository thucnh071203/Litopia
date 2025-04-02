using System;
using System.Collections.Generic;

namespace ChatService.Models;

public partial class GroupBook
{
    public Guid GroupBookId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? BookId { get; set; }

    public string? CurrentChapter { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Group? Group { get; set; }

    public virtual ICollection<GroupBookNote> GroupBookNotes { get; set; } = new List<GroupBookNote>();
}
