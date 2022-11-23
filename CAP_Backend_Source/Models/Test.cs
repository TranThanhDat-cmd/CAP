using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Test
{
    public int TestId { get; set; }

    public int ContentId { get; set; }

    public string TestTitle { get; set; } = null!;

    public int? Time { get; set; }

    public int Chapter { get; set; }

    public bool? IsRandom { get; set; }

    public virtual ContentProgram Content { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
