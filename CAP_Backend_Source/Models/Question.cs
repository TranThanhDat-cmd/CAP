using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? TestsId { get; set; }

    public int Type { get; set; }

    public string QuestionTitle { get; set; } = null!;

    public double? Score { get; set; }

    public virtual ICollection<Answer> Answers { get; } = new List<Answer>();

    public virtual Test? Tests { get; set; }
}
