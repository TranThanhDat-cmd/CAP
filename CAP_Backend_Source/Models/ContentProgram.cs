using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class ContentProgram
{
    public int ContentId { get; set; }

    public int ProgramId { get; set; }

    public int Chapter { get; set; }

    public string ContentType { get; set; } = null!;
    public string? ContentTitle { get; set; }
    public string? ContentDescription { get; set; }

    public string Content { get; set; } = null!;

    public virtual Program Program { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; } = new List<Test>();
}
