using System;
using System.Collections.Generic;

namespace PostService.Models;

public partial class Report
{
    public Guid ReportId { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? Status { get; set; }

    public Guid? ReviewedBy { get; set; }

    public DateTime? ReviewedDate { get; set; }

    public virtual Post Post { get; set; } = null!;
}
