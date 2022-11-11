using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class MultipleChoiceQuestion
{
    public int McquestionId { get; set; }

    public int TestsId { get; set; }

    public string McquestionTitle { get; set; } = null!;

    public string Content1 { get; set; } = null!;

    public string? Content2 { get; set; }

    public string? Content3 { get; set; }

    public string Content4 { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public virtual ICollection<MultipleChoiceAnswer> MultipleChoiceAnswers { get; } = new List<MultipleChoiceAnswer>();

    public virtual Test Tests { get; set; } = null!;
}
