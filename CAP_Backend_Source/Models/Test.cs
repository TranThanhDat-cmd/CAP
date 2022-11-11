using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Test
{
    public int TestId { get; set; }

    public int ProgramId { get; set; }

    public string TestTitle { get; set; } = null!;

    public int TypeId { get; set; }

    public virtual ICollection<EssayQuestion> EssayQuestions { get; } = new List<EssayQuestion>();

    public virtual ICollection<MultipleChoiceQuestion> MultipleChoiceQuestions { get; } = new List<MultipleChoiceQuestion>();

    public virtual Program Program { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;
}
