using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Test
{
    public int TestId { get; set; }

    public int ProgramId { get; set; }

    public string TestTitle { get; set; } = null!;

    public int TypeId { get; set; }

    public int? Time { get; set; }

    public int Chapter { get; set; }

    public virtual Program Program { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    public virtual Type Type { get; set; } = null!;
}
