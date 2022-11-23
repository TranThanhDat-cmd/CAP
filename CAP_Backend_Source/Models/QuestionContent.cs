using System;
using System.Collections.Generic;

namespace CAP_Backend_Source.Models;

public partial class QuestionContent
{
    public int QuestionContentId { get; set; }

    public int? QuestionId { get; set; }

    public string? Content { get; set; }

    public bool? IsAnswer { get; set; }

    public virtual Question? Question { get; set; }
}
